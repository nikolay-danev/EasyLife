using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyLife.Application.Services.Interfaces;
using EasyLife.Domain.Models;
using EasyLife.Persistence.Data;

namespace EasyLife.Test
{
	public class ServiceManager : IServiceManager
	{
		private readonly InMemoryContext db;

		public ServiceManager(InMemoryContext db)
		{
			this.db = db;
		}

		public async Task AddServiceAsync(Service service)
		{
			await this.db.Services.AddAsync(service);
			await this.db.SaveChangesAsync();
		}

		public ICollection<Service> GetAllServices()
		{
		     return this.db.Services.ToList();
		}

		public Task<Service> GetDetails(int id)
		{
			return this.db.Services.FindAsync(id);
		}

		public void RemoveService(Service service)
		{
			this.db.Services.Remove(service);
			this.db.SaveChanges();
		}

		public void UpdateService(Service service)
		{
			this.db.Services.Update(service);
			this.db.SaveChanges();
		}
	}
}
