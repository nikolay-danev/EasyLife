using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyLife.Application.Services;
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

		private static readonly ConnectionMapping<string> Connections =
			new ConnectionMapping<string>();

		public MessageHub(EasyLifeDbContext context, UserManager<User> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public override Task OnConnectedAsync()
		{
			string name = Context.User.Identity.Name;

			Connections.Add(name, Context.ConnectionId);

			return base.OnConnectedAsync();
		}

		public override Task OnDisconnectedAsync(Exception exception)
		{
			string name = Context.User.Identity.Name;

			Connections.Remove(name, Context.ConnectionId);
			return base.OnDisconnectedAsync(exception);
		}

		public async Task SendMessage(string senderEmail, string receiverEmail, string messageContent)
		{
			var sender = await this._userManager.Users.FirstOrDefaultAsync(x => x.Email == senderEmail);
			var receiver = await this._userManager.Users.FirstOrDefaultAsync(x => x.UserName == receiverEmail);
			var newMessage = new Message
			{
				Content = messageContent,
				CreatedOn = DateTime.UtcNow,
				ReceiverEmail = receiverEmail,
				Sender = sender,
				SenderId = sender.Id,
			};

			//await this._context.Messages.AddAsync(newMessage);
			//await this._context.SaveChangesAsync();
			//Connections.TryGetValue(receiverEmail, out var connectionToSendMessage);

			//if (!string.IsNullOrWhiteSpace(connectionToSendMessage))
			//{
			//await Clients.Caller.SendAsync("ReceiveMessage", newMessage);
			//await Clients.Group(receiver.UserName).SendAsync("ReceiveMessage", newMessage);
			//}

			foreach (var connectionId in Connections.GetConnections(receiver.UserName))
			{
				await Clients.Caller.SendAsync("ReceiveMessage", newMessage);
				await Clients.Client(connectionId).SendAsync("ReceiveMessage",  newMessage);
			}
		}


	public async Task GetMessages()
		{
			var caller = await this._userManager.Users.FirstOrDefaultAsync(x => x.Email == this.Context.User.Identity.Name);

			var messages = await this._context.Messages.Where(x => x.SenderId == caller.Id).ToArrayAsync();

			await this.Clients.Caller.SendAsync("ReceiveAllMessages", messages);
		}
	}
}
