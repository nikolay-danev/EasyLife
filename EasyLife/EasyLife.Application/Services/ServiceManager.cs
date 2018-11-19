using EasyLife.Application.Services.Interfaces;
using EasyLife.Domain.Models;
using EasyLife.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyLife.Application.Services
{
	public class ServiceManager : IServiceManager
	{
		private readonly EasyLifeDbContext db;

		public ServiceManager(EasyLifeDbContext db)
		{
			this.db = db;
		}

		public async Task AddServiceAsync(Service service)
		{
			await this.db.Services.AddAsync(service);
			await this.db.SaveChangesAsync();
		}

		public async Task GetDetails(int id)
		{
			await this.db.Services.FindAsync(id);
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
