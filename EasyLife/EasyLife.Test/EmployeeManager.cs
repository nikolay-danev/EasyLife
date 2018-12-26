using System.Collections.Generic;
using System.Threading.Tasks;
using EasyLife.Application.Services.Interfaces;
using EasyLife.Domain.Models;
using EasyLife.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace EasyLife.Test
{
	public class EmployeeManager : IEmployeeManager
	{
		private readonly InMemoryContext _dbContext;

		public EmployeeManager(InMemoryContext dbContext)
		{
			_dbContext = dbContext;
		}

		public Task CreateEmployeeAsync(Employee employee)
		{
			var result = _dbContext.Employees.AddAsync(employee);
			_dbContext.SaveChanges();
			return result;
		}

		public void RemoveEmployee(Employee employee)
		{
			employee.IsFired = true;
			_dbContext.SaveChanges();
		}

		public Task<List<Employee>> GetEmployees()
		{
			return _dbContext.Employees.ToListAsync();
		}

		public Task<Employee> GetEmployee(int id)
		{
			return _dbContext.Employees.FindAsync(id);
		}
	}
}
