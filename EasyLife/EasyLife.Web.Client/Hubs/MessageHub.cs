using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EasyLife.Application.Services;
using EasyLife.Domain.Models;
using EasyLife.Domain.ViewModels;
using EasyLife.Persistence.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EasyLife.Web.Client.Hubs
{
	[Authorize]
	public class MessageHub : Hub
	{
		private readonly EasyLifeDbContext _context;
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;

		private static readonly ConnectionMapping<string> Connections =
			new ConnectionMapping<string>();

		public MessageHub(EasyLifeDbContext context, UserManager<User> userManager, IMapper mapper)
		{
			_context = context;
			_userManager = userManager;
			_mapper = mapper;
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

			await Clients.Caller.SendAsync("ReceiveMessage", newMessage);
			foreach (var connectionId in Connections.GetConnections(receiver.UserName))
			{
				await Clients.Client(connectionId).SendAsync("AdminReceiveMessage", newMessage);
			}

			if (!string.IsNullOrEmpty(newMessage.Content))
			{
				await this._context.Messages.AddAsync(newMessage);
				await this._context.SaveChangesAsync();
			}
		}
		public async Task AdminSendMessage(string senderEmail, string receiverEmail, string messageContent)
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

			await Clients.Caller.SendAsync("AdminReceiveMessage", newMessage);
			foreach (var connectionId in Connections.GetConnections(receiver.UserName))
			{
				await Clients.Client(connectionId).SendAsync("ReceiveMessage", newMessage);
			}
			if (!string.IsNullOrEmpty(newMessage.Content))
			{
				await this._context.Messages.AddAsync(newMessage);
				await this._context.SaveChangesAsync();
			}
		}

		public async Task GetMessages()
		{
			var caller = await this._userManager.Users.FirstOrDefaultAsync(x => x.Email == this.Context.User.Identity.Name);

			var messages = await this._context.Messages.Where(x => x.SenderId == caller.Id || x.ReceiverEmail == caller.Email).ToListAsync();

			var info = _mapper.Map<List<MessageViewModel>>(messages);
			await Clients.Caller.SendAsync("getAll", info);
		}
	}
}
