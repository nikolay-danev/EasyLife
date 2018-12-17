using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyLife.Domain.Models;

namespace EasyLife.Application.Services.Interfaces
{
	public interface IMessageManager
	{
		Task<List<Message>> GetMessages(string userEmail);

		Task<Message> GetMessageInfo(int id);
	}
}
