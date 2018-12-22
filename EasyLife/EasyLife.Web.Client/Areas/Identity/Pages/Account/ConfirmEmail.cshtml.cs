using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyLife.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace EasyLife.Web.Client.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<User> _userManager;
	    private readonly SignInManager<User> _signInManager;
	    private readonly ILogger<LoginModel> _logger;
		public ConfirmEmailModel(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<LoginModel> logger)
        {
            _userManager = userManager;
			
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(string userId, string userCode)
        {
            if (userId == null || userCode == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, userCode);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Error confirming email for user with ID '{userId}':");
            }

			await _signInManager.SignInAsync(user, isPersistent: false);
			return Page();
		}
    }
}
