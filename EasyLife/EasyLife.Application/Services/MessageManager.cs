using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyLife.Application.Services.Interfaces;
using EasyLife.Domain.Models;
using EasyLife.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace EasyLife.Application.Services
{
	public class MessageManager : IMessageManager
	{
		private readonly EasyLifeDbContext _context;

		public MessageManager(EasyLifeDbContext context)
		{
			_context = context;
		}

		public Task<List<Message>> GetMessages(string userEmail)
		{
			return this._context.Messages.Include(x => x.Sender).Where(x => x.ReceiverEmail == userEmail || x.Sender.Email == userEmail).ToListAsync();
		}

		public Task<Message> GetMessageInfo(int id)
		{
			return this._context.Messages.FindAsync(id);
		}
	}
}
