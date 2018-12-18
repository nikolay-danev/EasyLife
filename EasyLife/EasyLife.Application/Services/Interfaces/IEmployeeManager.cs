using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EasyLife.Domain.GlobalConstants;
using EasyLife.Domain.Models;

namespace EasyLife.Application.Services.Interfaces
{
	public interface IEmployeeManager
	{
		Task CreateEmployeeAsync(Employee employee);
		void RemoveEmployee(Employee employee);
		Task<List<Employee>> GetEmployees();

	}
}
