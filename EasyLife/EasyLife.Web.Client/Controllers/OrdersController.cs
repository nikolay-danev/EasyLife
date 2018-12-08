using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EasyLife.Application.Services.Interfaces;
using EasyLife.Domain.GlobalConstants;
using EasyLife.Domain.GlobalConstatns;
using EasyLife.Domain.Models;
using EasyLife.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyLife.Web.Client.Controllers
{
	public class OrdersController : Controller
	{
		private readonly IOrderManager _orderManager;
		private readonly IMapper _mapper;
		private readonly UserManager<User> _userManager;

		public OrdersController(IOrderManager orderManager,
			IMapper mapper,
			UserManager<User> userManager)
		{
			_orderManager = orderManager;
			_mapper = mapper;
			_userManager = userManager;
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> OrderService(OrderViewModel model)
		{
			var currentUser =
				await this._userManager.Users.FirstOrDefaultAsync(x => x.UserName == this.User.Identity.Name);

			if (ModelState.IsValid)
			{
				var order = new Order
				{
					Address = model.Address,
					CreatedOn = DateTime.UtcNow,
					Status = OrderStatus.InProgress,
					ServiceType = model.ServiceType,
					Client = currentUser,
					ClientId = currentUser.Id
				};

				await this._orderManager.CreateOrderAsync(order);
				return this.Redirect("/Orders/MyOrders");
			}

			//If we get this far, something wrong happened
			return this.View($"/Services/Details/{model.Id}", model);
		}

		[Authorize]
		public async Task<IActionResult> MyOrders()
		{
			var currentUser =
				await this._userManager.Users.FirstOrDefaultAsync(x => x.UserName == this.User.Identity.Name);
			var userOrders = await this._orderManager.GetOrdersAsync(currentUser);

			var viewModels = _mapper.Map<List<OrderViewModel>>(userOrders);

			return this.View(viewModels);
		}

		[Authorize(Roles = RoleType.Administrator)]
		public async Task<IActionResult> AllOrders(string searchString = null)
		{
			if (string.IsNullOrEmpty(searchString))
			{
				var orders = await this._orderManager.GetOrdersAsync();

				var viewModels = _mapper.Map<List<OrderViewModel>>(orders);

				return this.View(viewModels);
			}
			else
			{
				var orders = await this._orderManager.GetOrdersAsync();

				var filteredOrders = orders
					.Where(x => x.Address.Contains(searchString) || x.ServiceType.Contains(searchString))
					.ToList();

				var viewModels = _mapper.Map<List<OrderViewModel>>(filteredOrders);

				return this.View(viewModels);
			}
		}

		[Authorize(Roles = RoleType.Administrator)]
		public async Task<IActionResult> CompleteOrder(int id)
		{
			var order = await _orderManager.GetOrderAsync(id);
			order.Status = OrderStatus.Completed;
			this._orderManager.UpdateOrderStatus(order);
			return this.Redirect("/Orders/AllOrders");
		}

		[Authorize(Roles = RoleType.Administrator)]
		public async Task<IActionResult> DeleteOrder(int id)
		{
			var order = await _orderManager.GetOrderAsync(id);
			this._orderManager.DeleteOrder(order);
			return this.Redirect("/Orders/AllOrders");
		}
	}
}