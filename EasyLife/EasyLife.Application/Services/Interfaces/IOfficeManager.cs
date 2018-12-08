using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyLife.Domain.Models;
using EasyLife.Domain.ViewModels;

namespace EasyLife.Application.Services.Interfaces
{
	public interface IOfficeManager
	{
		Task<List<Office>> GetOfficesAsync();

		Task AddOfficeAsync(Office office);

		Task<Office> GetOfficeAsync(int id);

		void UpdateOffice(Office office);

		void DeleteOffice(Office office);
	}
}
