using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyLife.Domain.ViewModels;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace EasyLife.Web.Client.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

		[ResponseCache(Duration = 120, Location = ResponseCacheLocation.Client)]
        public IActionResult About()
        {
            return View();
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
