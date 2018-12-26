using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyLife.Application.Services.Interfaces;
using EasyLife.Domain.Models;
using EasyLife.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace EasyLife.Test
{
	public class OrderManager : IOrderManager
	{
		private readonly InMemoryContext db;

		public OrderManager(InMemoryContext db)
		{
			this.db = db;
		}

		public Task<List<Order>> GetOrdersAsync(User user = null)
		{
			if (user != null)
			{
				return this.db.Orders.Include(x => x.Client).Where(x => x.Client == user).ToListAsync();
			}
			return this.db.Orders.Include(x => x.Client).ToListAsync();
		}

		public Task CreateOrderAsync(Order order)
		{
			this.db.Orders.AddAsync(order);
			return this.db.SaveChangesAsync();
		}

		public Task<Order> GetOrderAsync(int id)
		{
			return this.db.Orders.FindAsync(id);
		}

		public void DeleteOrder(Order order)
		{
			this.db.Orders.Remove(order);
			this.db.SaveChanges();
		}

		public void UpdateOrderStatus(Order order)
		{
			this.db.Orders.Update(order);
			this.db.SaveChanges();
		}
	}
}
