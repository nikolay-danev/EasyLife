using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyLife.Application.Services.Interfaces;
using EasyLife.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyLife.Test
{
	public class OfficeManager : IOfficeManager
	{
		private readonly InMemoryContext _dbContext;

		public OfficeManager(InMemoryContext dbContext)
		{
			_dbContext = dbContext;
		}
		public Task<List<Office>> GetOfficesAsync()
		{
			return _dbContext.Offices.ToListAsync();
		}

		public Task AddOfficeAsync(Office office)
		{
			var result = _dbContext.Offices.AddAsync(office);
			_dbContext.SaveChanges();
			return result;
		}

		public Task<Office> GetOfficeAsync(int id)
		{
			throw new NotImplementedException();
		}

		public void UpdateOffice(Office office)
		{
			throw new NotImplementedException();
		}

		public void DeleteOffice(Office office)
		{
			throw new NotImplementedException();
		}
	}
}
