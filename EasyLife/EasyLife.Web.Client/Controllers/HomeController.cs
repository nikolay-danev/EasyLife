using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EasyLife.Application.Services;
using EasyLife.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using EasyLife.Domain.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace EasyLife.Web.Client.Controllers
{
    public class HomeController : Controller
    {
	    private readonly IAdvertisementManager _advertisementManager;
	    private readonly IMapper _mapper;
	    private readonly IHostingEnvironment _host;
	    private readonly IEmployeeManager _employeeManager;

	    public HomeController(IAdvertisementManager advertisementManager,
		    IMapper mapper,
		    IHostingEnvironment host, IEmployeeManager employeeManager)
	    {
		    _advertisementManager = advertisementManager;
		    _mapper = mapper;
		    _host = host;
		    _employeeManager = employeeManager;
	    }

        public async Task<IActionResult> Index()
        {
	        var models = await this._advertisementManager.All();
	        var viewModels = new List<AdvertisementViewModel>();

	        foreach (var advertisement in models)
	        {
		        advertisement.ImageUrl = ImageUrlRefactor.RefactorUrl(advertisement, _host.WebRootPath);
		        viewModels.Add(_mapper.Map<AdvertisementViewModel>(advertisement));
	        }

            return View(viewModels);
        }

		[ResponseCache(Duration = 120, Location = ResponseCacheLocation.Client)]
        public async Task<IActionResult> About()
        {
	        var employees = await _employeeManager.GetEmployees();
	        var employeesViews = _mapper.Map<List<EmployeeViewModel>>(employees);
	        return View(employeesViews);
        }

	    [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Client)]
		public IActionResult Contact()
        {
            return View();
        }

	    [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Client)]
		public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
