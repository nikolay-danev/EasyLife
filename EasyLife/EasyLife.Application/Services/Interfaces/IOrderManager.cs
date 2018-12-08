using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyLife.Domain.Models;

namespace EasyLife.Application.Services.Interfaces
{
	public interface IOrderManager
	{
		Task<List<Order>> GetOrdersAsync(User user = null);

		Task CreateOrderAsync(Order order);

		Task<Order> GetOrderAsync(int id);

		void DeleteOrder(Order order);

		void UpdateOrderStatus(Order order);
	}
}
