using EasyLife.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyLife.Application.Services.Interfaces
{
	public interface IAdvertisementManager
	{
		Task CreateAdvertisement(Advertisement advertisement);

		void DeleteAdvertisement(Advertisement advertisement);

		Task<Advertisement> Details(int id);

		void UpdateAdvertisement(Advertisement advertisement);

		Task<List<Advertisement>> All(User creator = null);
	}
}
