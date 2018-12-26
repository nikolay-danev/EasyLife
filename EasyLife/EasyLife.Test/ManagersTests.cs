using System;
using System.Linq;
using EasyLife.Domain.Models;
using EasyLife.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace EasyLife.Test
{
	public class ManagersTests
	{
		[Fact]
		public async void OfficeManagerTestAddingOfficeCorrectly()
		{
			var options = new DbContextOptionsBuilder<EasyLifeDbContext>()
				.UseInMemoryDatabase(databaseName: "Find_User_Database") // Give a Unique name to the DB
				.Options;

			using (var dbContext = new InMemoryContext(options)) // Initialize Testing Data
			{
				var officeManager = new OfficeManager(dbContext);
				await officeManager.AddOfficeAsync(new Office
				{
					Address = "asd",
					PhoneNumber = "123",
				});
				var list = await officeManager.GetOfficesAsync();
				var count = list.Count;
				Assert.Equal(1, count);
			}

		}

		[Fact]
		public async void AdvertisementManagerTestAddingAdCorrectly()
		{
			var options = new DbContextOptionsBuilder<EasyLifeDbContext>()
				.UseInMemoryDatabase(databaseName: "Find_User_Database") // Give a Unique name to the DB
				.Options;

			using (var dbContext = new InMemoryContext(options)) // Initialize Testing Data
			{
				var advertisementManager = new AdvertisementManager(dbContext);
				await advertisementManager.CreateAdvertisement(new Advertisement
				{
					BusinessName = "asd",
					CreatedOn = DateTime.Today,
					CreatorId = "asd",
					ExpirationDate = DateTime.Today.AddDays(1),
					ImageUrl = "asd",
					Url = "asd",
				});
				var list = await advertisementManager.All();
				var count = list.Count;
				Assert.Equal(1, count);
			}

		}

		[Fact]
		public async void EmployeeManagerTestAddingEmployeeCorrectly()
		{
			var options = new DbContextOptionsBuilder<EasyLifeDbContext>()
				.UseInMemoryDatabase(databaseName: "Find_User_Database") // Give a Unique name to the DB
				.Options;

			using (var dbContext = new InMemoryContext(options)) // Initialize Testing Data
			{
				var employeeManager = new EmployeeManager(dbContext);
				await employeeManager.CreateEmployeeAsync(new Employee
				{
					CreatedOn = DateTime.Today,
					FacebookUrl = "asd",
					FullName = "Gosho",
					IsFired = false,
					JobPosition = "asd",
					ProfilePictureUrl = "asd",
					ShortDescription = "asd"
				});
				var list = await employeeManager.GetEmployees();
				var count = list.Count;
				Assert.Equal(1, count);
			}
		}

		[Fact]
		public async void OrderManagerTestAddingOrderCorrectly()
		{
			var options = new DbContextOptionsBuilder<EasyLifeDbContext>()
				.UseInMemoryDatabase(databaseName: "Find_User_Database") // Give a Unique name to the DB
				.Options;

			using (var dbContext = new InMemoryContext(options)) // Initialize Testing Data
			{
				var orderManager = new OrderManager(dbContext);
				await orderManager.CreateOrderAsync(new Order
				{
					CreatedOn = DateTime.Today,
					Address = "asd",
					ClientId = "asd",
					ServiceType = "asd",
					Status = "asd",
					Client = new User()
				});
				var list = await orderManager.GetOrdersAsync();
				var count = list.Count;
				Assert.Equal(1, count);
			}
		}

		[Fact]
		public async void ServiceManagerTestAddingServiceCorrectly()
		{
			var options = new DbContextOptionsBuilder<EasyLifeDbContext>()
				.UseInMemoryDatabase(databaseName: "Find_User_Database") // Give a Unique name to the DB
				.Options;

			using (var dbContext = new InMemoryContext(options)) // Initialize Testing Data
			{
				var serviceManager = new ServiceManager(dbContext);
				await serviceManager.AddServiceAsync(new Service()
				{
					CreatedOn = DateTime.Today,
					ServiceType = "asd",
					AdditionalDescription = "asd",
					Description = "asd",
					ServiceTitle = "asd"
				});
				var list = serviceManager.GetAllServices();
				var count = list.Count;
				Assert.Equal(1, count);
			}
		}
	}
}
