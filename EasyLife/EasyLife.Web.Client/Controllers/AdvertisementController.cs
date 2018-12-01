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
using AutoMapper;
using EasyLife.Domain.GlobalConstants;

namespace EasyLife.Web.Client.Controllers
{
	[Authorize]
	public class AdvertisementController : Controller
	{
		private readonly IAdvertisementManager advertisementManager;
		private readonly UserManager<User> userManager;
		private readonly IHostingEnvironment host;
		private readonly IMapper mapper;


		public AdvertisementController(IAdvertisementManager advertisementManager,
			UserManager<User> userManager,
			IHostingEnvironment host,
			IMapper mapper)
		{
			this.advertisementManager = advertisementManager;
			this.userManager = userManager;
			this.host = host;
			this.mapper = mapper;
		}

		[Authorize(Roles =  "Administrator")]
		public async Task<IActionResult> Index()
		{
			var ads = await this.advertisementManager.All();

			var adsModels = mapper.Map<List<AdvertisementViewModel>>(ads);

			return this.View(adsModels);
		}

		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> Delete(int id)
		{
			var ad = await this.advertisementManager.Details(id);

			this.advertisementManager.DeleteAdvertisement(ad);

			return Redirect("/Advertisement/Index");
		}

		public IActionResult Create()
		{
			return this.View();
		}

		public async Task<IActionResult> MyAds()
		{
			var creator = this.userManager.Users.FirstOrDefault(x => x.UserName == this.User.Identity.Name);

			var userAdvertisements = await this.advertisementManager.All(creator);

			var models = mapper.Map<List<AdvertisementViewModel>>(userAdvertisements);

			return this.View(models);
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
			var userRoles = await this.userManager.GetRolesAsync(user);
			advertisement.ImageUrl = advertisement.ImageUrl.Replace(host.WebRootPath, "");

			if (advertisement == null || (advertisement.Creator != user && !userRoles.Contains(RoleType.Administrator)))
			{
				return this.Redirect("/Home/Index");
			}

			var viewModel = mapper.Map<AdvertisementViewModel>(advertisement);

			return this.View(viewModel);
		}
	}
}
