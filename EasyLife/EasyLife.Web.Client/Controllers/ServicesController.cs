using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EasyLife.Application.Services;
using EasyLife.Application.Services.Interfaces;
using EasyLife.Domain.GlobalConstants;
using EasyLife.Domain.Models;
using EasyLife.Domain.ViewModels;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace EasyLife.Web.Client.Controllers
{
	public class ServicesController : Controller
	{
		private readonly IHostingEnvironment host;
		private readonly IServiceManager serviceManager;

		public ServicesController(IHostingEnvironment host,
					IServiceManager serviceManager)
		{
			this.host = host;
			this.serviceManager = serviceManager;
		}

		[Authorize(Roles = RoleType.Administrator)]
		public IActionResult Create()
		{
			return this.View();
		}

		[HttpPost]
		[Authorize(Roles = RoleType.Administrator)]
		public async Task<IActionResult> Create(ServiceViewModel model)
		{
			if (ModelState.IsValid)
			{
				var service = new Service
				{
					ServiceTitle = model.ServiceTitle,
					ServiceType = model.ServiceType,
					Description = model.Description,
					AdditionalDescription = model.AdditionalDescription,
					CreatedOn = DateTime.UtcNow
				};

				var filePath = host.WebRootPath + "/images/" + $"{service.ServiceTitle.Replace(" ","").ToLower()}.jpg";

				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await model.MainImage.CopyToAsync(stream);
				}

				await this.serviceManager.AddServiceAsync(service);
			}
			return this.Redirect("/Services/Index");
		}

		public IActionResult Index()
		{
			var services = this.serviceManager.GetAllServices();

			var servicesModels = new List<ServiceViewModel>();

			foreach (var service in services)
			{
				var imageUrl = GetImageUrl(service.ServiceTitle);
				servicesModels.Add(new ServiceViewModel
				{
					Id = service.Id,
					ServiceTitle = service.ServiceTitle,
					ServiceType = service.ServiceType,
					Description = service.Description,
					ImageUrl = imageUrl
				});
			}

			return this.View(servicesModels);
		}


		public async Task<IActionResult> Details(int id)
		{
			var service = await this.serviceManager.GetDetails(id);

			var imageUrl = GetImageUrl(service.ServiceTitle);

			var model = new ServiceViewModel
			{
				Description = service.Description,
				ServiceTitle = service.ServiceTitle,
				ServiceType = service.ServiceType,
				AdditionalDescription = service.AdditionalDescription,
				ImageUrl = imageUrl,
			};

			return this.View(model);
		}

		private string GetImageUrl(string serviceTitle)
		{
			return "/images/" + $"{serviceTitle.Replace(" ", "").ToLower()}.jpg";
		}
	}
}