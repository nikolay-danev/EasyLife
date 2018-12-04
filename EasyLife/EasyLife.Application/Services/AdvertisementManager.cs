using EasyLife.Application.Services.Interfaces;
using EasyLife.Domain.Models;
using EasyLife.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
			return this.db.Advertisements.Include(x => x.Creator).FirstOrDefaultAsync(x => x.Id == id);
		}

		public void UpdateAdvertisement(Advertisement advertisement)
		{
			 this.db.Advertisements.Update(advertisement);
			this.db.SaveChanges();
		}

		public Task<List<Advertisement>> All(User creator)
		{
			if (creator != null)
			{
				return this.db.Advertisements.Include(x => x.Creator).Where(x => x.Creator == creator).ToListAsync();
			}
			return this.db.Advertisements.Include(x => x.Creator).ToListAsync();
		}

	}
}
