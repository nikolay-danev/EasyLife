using EasyLife.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyLife.Application.Services.Interfaces
{
	public interface IServiceManager
	{
		Task AddServiceAsync(Service service);

		void RemoveService(Service service);

		Task GetDetails(int id);

		void UpdateService(Service service);
	}
}
