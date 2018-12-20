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
		public async void OfficeManagerTestAddingOffice()
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
	}
}
