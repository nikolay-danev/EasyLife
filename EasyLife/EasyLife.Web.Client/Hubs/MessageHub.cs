using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyLife.Domain.Models;
using EasyLife.Persistence.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace EasyLife.Web.Client.Hubs
{
	[Authorize]
	public class MessageHub : Hub
	{
		private readonly EasyLifeDbContext _context;
		private readonly UserManager<User> _userManager;

		public MessageHub(EasyLifeDbContext context, UserManager<User> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public async Task SendMessage(string senderEmail, string receiverEmail, string messageContent)
		{
			var sender = await this._userManager.Users.FirstOrDefaultAsync(x => x.Email == senderEmail);
			var receiver = await this._userManager.Users.FirstOrDefaultAsync(x => x.Email == receiverEmail);
			var newMessage = new Message
			{
				Content = messageContent,
				CreatedOn = DateTime.UtcNow,
				ReceiverEmail = receiverEmail,
				Sender = sender,
				SenderId = sender.Id,
			};

			await this._context.Messages.AddAsync(newMessage);

			await this._context.SaveChangesAsync();

			await this.Clients.Caller.SendAsync("ReceiveMessage", newMessage.Content);
		}

		public async Task GetMessages()
		{
			var caller = await this._userManager.Users.FirstOrDefaultAsync(x => x.Email == this.Context.User.Identity.Name);

			var messages = await this._context.Messages.Where(x => x.SenderId == caller.Id).FirstOrDefaultAsync();

			await this.Clients.Caller.SendAsync("ReceiveAllMessages", messages.Content);
		}
	}
}
