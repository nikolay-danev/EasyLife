using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EasyLife.Application.Services.Interfaces;
using EasyLife.Domain.GlobalConstants;
using EasyLife.Domain.Models;
using EasyLife.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace EasyLife.Web.Client.Controllers
{
	[Authorize(Roles = RoleType.Administrator)]
    public class EmployeeController : Controller
    {
	    private readonly IHostingEnvironment _host;
	    private readonly IEmployeeManager _employeeManager;
	    private readonly IMapper _mapper;

	    public EmployeeController(IHostingEnvironment host, IEmployeeManager employeeManager, IMapper mapper)
	    {
		    _host = host;
		    _employeeManager = employeeManager;
		    _mapper = mapper;
	    }

        public async Task<IActionResult> Index()
        {
	        var employees = await _employeeManager.GetEmployees();
	        var employeesViews = _mapper.Map<List<EmployeeViewModel>>(employees);
            return View(employeesViews);
        }

	    public IActionResult CreateEmployee()
	    {
		    return this.View();
	    }

		[HttpPost]
	    public async Task<IActionResult> Fire([FromBody]int id)
		{
			var employee = await _employeeManager.GetEmployee(id);
			if (employee != null)
			{
				_employeeManager.RemoveEmployee(employee);
			}
			return Redirect("/Home/About");
		}

		[HttpPost]
	    public async Task<IActionResult> CreateEmployee(EmployeeViewModel model)
	    {
		    if (ModelState.IsValid)
		    {
			    var directoryPath = _host.WebRootPath + "/images/EmployeesProfilePictures/";
			    var filePath = directoryPath + $"{model.FullName.Replace(" ", "")}.jpg";

			    if (!Directory.Exists(directoryPath))
			    {
				    Directory.CreateDirectory(directoryPath);
			    }

			    using (var stream = new FileStream(filePath, FileMode.Create))
			    {
				    await model.ProfilePicture.CopyToAsync(stream);
			    }

			    var employee = new Employee
			    {
					CreatedOn = DateTime.UtcNow,
					FullName = model.FullName,
					FacebookUrl = model.FacebookUrl,
					JobPosition = model.JobPosition,
					ShortDescription = model.ShortDescription,
					IsFired = false,
					ProfilePictureUrl = "/images/EmployeesProfilePictures/" + $"{model.FullName.Replace(" ", "")}.jpg"
				};

			    await _employeeManager.CreateEmployeeAsync(employee);

			    return Redirect("/Employee/");
			}

		    return View(model);
	    }
    }
}