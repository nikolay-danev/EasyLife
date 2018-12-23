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
using EasyLife.Application.Services;
using EasyLife.Domain.GlobalConstants;
using EasyLife.Domain.GlobalConstatns;

namespace EasyLife.Web.Client.Controllers
{
	[Authorize]
	public class AdvertisementController : Controller
	{
		private readonly IAdvertisementManager _advertisementManager;
		private readonly UserManager<User> _userManager;
		private readonly IHostingEnvironment _host;
		private readonly IMapper _mapper;

		public AdvertisementController(IAdvertisementManager advertisementManager,
			UserManager<User> userManager,
			IHostingEnvironment host,
			IMapper mapper)
		{
			this._advertisementManager = advertisementManager;
			this._userManager = userManager;
			this._host = host;
			this._mapper = mapper;
		}

		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> Index()
		{
			var ads = await this._advertisementManager.All();

			var adsModels = _mapper.Map<List<AdvertisementViewModel>>(ads);

			return this.View(adsModels);
		}

		[Authorize(Roles = "Administrator")]
		public async Task<IActionResult> Delete(int id)
		{
			var ad = await this._advertisementManager.Details(id);

			this._advertisementManager.DeleteAdvertisement(ad);

			return Redirect("/Advertisement/Index");
		}

		public IActionResult Create()
		{
			return this.View();
		}

		public async Task<IActionResult> MyAds()
		{
			var creator = this._userManager.Users.FirstOrDefault(x => x.UserName == this.User.Identity.Name);

			var userAdvertisements = await this._advertisementManager.All(creator);

			var models = _mapper.Map<List<AdvertisementViewModel>>(userAdvertisements);

			return this.View(models);
		}

		[HttpPost]
		public async Task<IActionResult> Create(AdvertisementViewModel model)
		{
			if (ModelState.IsValid)
			{
				var currentUser = this._userManager.Users.FirstOrDefault(x => x.UserName == this.User.Identity.Name);

				var directoryPath = _host.WebRootPath + "/images/advertisementImages/";
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
					ImageUrl = "/images/advertisementImages/" + $"{model.BusinessName.Replace(" ", "")}.jpg",
					BusinessName = model.BusinessName
				};

				await this._advertisementManager.CreateAdvertisement(advertisement);

				return this.Redirect($"/Advertisement/Details/{advertisement.Id}");
			}
			//If we get this far, something's wrong!
			return this.View(model);
		}

		public async Task<IActionResult> Details(int id)
		{
			var advertisement = await this._advertisementManager.Details(id);

			var user = this._userManager.Users.FirstOrDefault(x => x.UserName == this.User.Identity.Name);
			var userRoles = await this._userManager.GetRolesAsync(user);

			if (advertisement == null || (advertisement.Creator != user && !userRoles.Contains(RoleType.Administrator)))
			{
				return this.Redirect("/Home/Index");
			}

			var viewModel = _mapper.Map<AdvertisementViewModel>(advertisement);

			return this.View(viewModel);
		}

		public async Task<IActionResult> Edit(int id)
		{
			var advertisement = await this._advertisementManager.Details(id);

			var user = this._userManager.Users.FirstOrDefault(x => x.UserName == this.User.Identity.Name);
			var userRoles = await this._userManager.GetRolesAsync(user);
			advertisement.ImageUrl = advertisement.ImageUrl.Replace(_host.WebRootPath, "");

			if (advertisement == null || (advertisement.Creator != user && !userRoles.Contains(RoleType.Administrator)))
			{
				return this.Redirect("/Home/Index");
			}

			var viewModel = _mapper.Map<AdvertisementViewModel>(advertisement);

			return this.View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(AdvertisementViewModel model)
		{
			var advertisement = await this._advertisementManager.Details(model.Id);

			var user = this._userManager.Users.FirstOrDefault(x => x.UserName == this.User.Identity.Name);
			var userRoles = await this._userManager.GetRolesAsync(user);

			var isChanged = false;

			advertisement.ImageUrl = advertisement.ImageUrl.Replace(_host.WebRootPath, "");

			if (advertisement == null || (advertisement.Creator != user && !userRoles.Contains(RoleType.Administrator)))
			{
				return this.Redirect("/Home/Index");
			}

			if (advertisement.BusinessName != model.BusinessName)
			{
				advertisement.BusinessName = model.BusinessName;
				isChanged = true;
			}

			if (advertisement.Url != model.Url)
			{
				advertisement.Url = model.Url;
				isChanged = true;
			}

			if (model.ImageFile != null)
			{

				var directoryPath = _host.WebRootPath + "/images/advertisementImages/";
				var filePath = directoryPath + $"{model.BusinessName.Replace(" ", "")}.jpg";

				var currentImage = new FileInfo(filePath);

				currentImage.Delete();

				if (!Directory.Exists(directoryPath))
				{
					Directory.CreateDirectory(directoryPath);
				}

				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await model.ImageFile.CopyToAsync(stream);
				}

				advertisement.ImageUrl = filePath;
				isChanged = true;
			}

			if (isChanged)
			{
				this._advertisementManager.UpdateAdvertisement(advertisement);
			}

			return Redirect("/Advertisement/Details/" + advertisement.Id);
		}

		public async Task<IActionResult> Renew(int id)
		{
			var ad = await this._advertisementManager.Details(id);

			if (ad == null || DateTime.UtcNow < ad.ExpirationDate)
			{
				return this.Redirect($"/Advertisement/Details/{id}");
			}

			var model = _mapper.Map<AdvertisementViewModel>(ad);
			return this.View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Renew(int id, string subscriptionPlan)
		{
			var ad = await this._advertisementManager.Details(id);

			var user = ad.Creator;

			if (ad == null || DateTime.UtcNow < ad.ExpirationDate)
			{
				return this.Redirect($"/Advertisement/Details/{id}");
			}

			SetUserDiscount(user, subscriptionPlan);

			UpdateAd(ad, subscriptionPlan);

			return this.Redirect($"/Advertisement/Details/{id}");
		}

		private void UpdateAd(Advertisement ad, string subscriptionPlan)
		{
			if (subscriptionPlan == SubscriptionPlan.Basic)
			{
				ad.ExpirationDate = DateTime.UtcNow.AddDays(15);
			}
			else if (subscriptionPlan == SubscriptionPlan.Advanced)
			{
				ad.ExpirationDate = DateTime.UtcNow.AddMonths(6);
			}
			else if (subscriptionPlan == SubscriptionPlan.Pro)
			{
				ad.ExpirationDate = DateTime.UtcNow.AddYears(1);
			}

			this._advertisementManager.UpdateAdvertisement(ad);
		}

		private void SetUserDiscount(User user, string subscriptionPlan)
		{
			if (subscriptionPlan == SubscriptionPlan.Basic)
			{
				user.Discount = 0.1;
			}
			else if (subscriptionPlan == SubscriptionPlan.Advanced)
			{
				user.Discount = 0.15;
			}
			else if (subscriptionPlan == SubscriptionPlan.Pro)
			{
				user.Discount = 0.25;
			}

			this._userManager.UpdateAsync(user);
		}
	}
}
