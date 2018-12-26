using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyLife.Application.Services.Interfaces;
using EasyLife.Domain.Models;
using EasyLife.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace EasyLife.Test
{
	public class AdvertisementManager : IAdvertisementManager
	{
		private readonly InMemoryContext db;

		public AdvertisementManager(InMemoryContext db)
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

		public Task<List<Advertisement>> All(User creator = null)
		{
			if (creator != null)
			{
				return this.db.Advertisements.Include(x => x.Creator).Where(x => x.Creator == creator).ToListAsync();
			}
			return this.db.Advertisements.Include(x => x.Creator).ToListAsync();
		}

	}
}
