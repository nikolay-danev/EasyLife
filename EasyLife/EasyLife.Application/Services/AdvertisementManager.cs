using EasyLife.Application.Services.Interfaces;
using EasyLife.Domain.Models;
using EasyLife.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyLife.Application.Services
{
	public class AdvertisementManager : IAdvertisementManager
	{
		private readonly EasyLifeDbContext db;

		public AdvertisementManager(EasyLifeDbContext db)
		{
			this.db = db;
		}

		public Task CreateAdvertisement(Advertisement advertisement)
		{
			this.db.Advertisements.AddAsync(advertisement);
			return this.db.SaveChangesAsync();
		}

		public void DeleteAdvertisement(Advertisement advertisement)
		{
			this.db.Advertisements.Remove(advertisement);
			this.db.SaveChanges();
		}

		public Task<Advertisement> Details(int id)
		{
			return this.db.Advertisements.FindAsync(id);
		}

		public void UpdateAdvertisement(Advertisement advertisement)
		{
			 this.db.Advertisements.Update(advertisement);
			this.db.SaveChanges();
		}
	}
}
