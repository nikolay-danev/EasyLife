using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EasyLife.Application.Services.Interfaces;
using EasyLife.Domain.Models;
using EasyLife.Domain.ViewModels;
using EasyLife.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace EasyLife.Application.Services
{
	public class OfficeManager : IOfficeManager
	{
		private readonly EasyLifeDbContext _context;
		private readonly IMapper _mapper;

		public OfficeManager(EasyLifeDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public Task<List<Office>> GetOfficesAsync()
		{
			return _context.Offices.ToListAsync();
		}

		public Task AddOfficeAsync(Office office)
		{
			var result = _context.Offices.AddAsync(office);
			 _context.SaveChanges();
			return result;
		}

		public Task<Office> GetOfficeAsync(int id)
		{
			return _context.Offices.FindAsync(id);
		}

		public void UpdateOffice(Office office)
		{
			_context.Offices.Update(office);
			_context.SaveChanges();
		}

		public void DeleteOffice(Office office)
		{
			_context.Offices.Remove(office);
			_context.SaveChanges();
		}
	}
}
