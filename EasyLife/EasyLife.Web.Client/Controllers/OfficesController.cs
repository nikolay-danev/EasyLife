using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EasyLife.Application.Services.Interfaces;
using EasyLife.Domain.GlobalConstants;
using EasyLife.Domain.Models;
using EasyLife.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyLife.Web.Client.Controllers
{
    public class OfficesController : Controller
    {
	    private readonly IOfficeManager _officeManager;
	    private readonly IMapper _mapper;

	    public OfficesController(IOfficeManager officeManager, IMapper mapper)
	    {
		    _officeManager = officeManager;
		    _mapper = mapper;
	    }

        public async Task<IActionResult> Index()
        {
	        var offices = await _officeManager.GetOfficesAsync();

	        var officesViewModels = _mapper.Map<List<OfficeViewModel>>(offices);

            return View(officesViewModels);
        }

	    [Authorize(Roles = RoleType.Administrator)]
	    public async Task<IActionResult> Delete(int id)
	    {
		    var office = await _officeManager.GetOfficeAsync(id);

			_officeManager.DeleteOffice(office);

		    return this.Redirect("/Offices/");
	    }


		[Authorize(Roles = RoleType.Administrator)]
	    public IActionResult AddOffice()
	    {
		    return this.View();
	    }

	    [Authorize(Roles = RoleType.Administrator)]
	    public async Task<IActionResult> Update(int id)
	    {
		    var office = await _officeManager.GetOfficeAsync(id);

		    var officeModel = _mapper.Map<OfficeViewModel>(office);

		    return this.View(officeModel);
	    }

	    [Authorize(Roles = RoleType.Administrator)]
		[HttpPost]
	    public async Task<IActionResult> Update(OfficeViewModel officeViewModel)
	    {
		    var office = await _officeManager.GetOfficeAsync(officeViewModel.Id);

		    if (office.Address != officeViewModel.Address)
		    {
			    office.Address = officeViewModel.Address;
				_officeManager.UpdateOffice(office);
		    }

		    if (office.PhoneNumber != officeViewModel.PhoneNumber)
		    {
			    office.PhoneNumber = officeViewModel.PhoneNumber;
			    _officeManager.UpdateOffice(office);
			}

		    return this.Redirect("/Offices/");
	    }

		[Authorize(Roles = RoleType.Administrator)]
		[HttpPost]
	    public async Task<IActionResult> AddOffice(OfficeViewModel officeViewModel)
	    {
		    if (ModelState.IsValid)
		    {
			    var newOffice = new Office
			    {
					Address = officeViewModel.Address,
					PhoneNumber = officeViewModel.PhoneNumber
			    };

			    await _officeManager.AddOfficeAsync(newOffice);

			    return Redirect("/Offices/");
		    }
			//If we get this far - something wrong happened!
		    return this.View(officeViewModel);
	    }
	}
}