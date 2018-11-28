using EasyLife.Application.Services;
using EasyLife.Application.Services.Interfaces;
using EasyLife.Domain.Models;
using EasyLife.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLife.Web.Client.Controllers
{
	[Authorize]
	public class AdvertisementController : Controller
	{
		private readonly IAdvertisementManager advertisementManager;
		private readonly UserManager<User> userManager;
		private readonly IHostingEnvironment host;

		public AdvertisementController(IAdvertisementManager advertisementManager, UserManager<User> userManager, IHostingEnvironment host)
		{
			this.advertisementManager = advertisementManager;
			this.userManager = userManager;
			this.host = host;
		}

		[Authorize(Roles =  "Administrator")]
		public IActionResult Index()
		{
			return this.View();
		}

		public IActionResult Create()
		{
			return this.View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(AdvertisementViewModel model)
		{
			if (ModelState.IsValid)
			{
				var currentUser = this.userManager.Users.FirstOrDefault(x => x.UserName == this.User.Identity.Name);

				var directoryPath = host.WebRootPath + "/images/advertisementImages/";
				var filePath = directoryPath + $"{model.BusinessName.Replace(" ", "")}.jpg";

				if (!Directory.Exists(directoryPath))
				{
					Directory.CreateDirectory(directoryPath);
				}

					using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await model.ImageFile.CopyToAsync(stream);
				}

				var advertisement = new Advertisement
				{
					Url = model.Url,
					CreatedOn = DateTime.UtcNow,
					Creator = currentUser,
					ExpirationDate = DateTime.UtcNow.AddDays(5),
					ImageUrl = filePath,
					BusinessName = model.BusinessName
				};
				await this.advertisementManager.CreateAdvertisement(advertisement);

				return this.Redirect($"/Advertisement/Details/{advertisement.Id}");
			}
			//If we get this far, something's wrong!
			return this.View(model);
		}

		public async Task<IActionResult> Details(int id)
		{
			var advertisement = await this.advertisementManager.Details(id);
			var user = this.userManager.Users.FirstOrDefault(x => x.UserName == this.User.Identity.Name);

			if (advertisement.Creator != user)
			{
				return this.Redirect("/Home/Index");
			}
			
			var model = new AdvertisementViewModel
			{
				BusinessName = advertisement.BusinessName,
				ExpirationDate = advertisement.ExpirationDate,
				Url = advertisement.Url,
				ImageUrl = advertisement.ImageUrl.Replace(host.WebRootPath,""),
				CreatedOn = advertisement.CreatedOn
			};

			return this.View(model);
		}
	}
}
