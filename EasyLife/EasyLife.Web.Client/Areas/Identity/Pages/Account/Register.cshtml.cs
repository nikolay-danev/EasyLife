﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using EasyLife.Application.Services;
using EasyLife.Domain.GlobalConstants;
using EasyLife.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace EasyLife.Web.Client.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Username is required.")]
            [StringLength(60, ErrorMessage = "Username cannot be more than 60 characters.")]
            [Display(Name = "Username")]
            public string Nickname { get; set; }

            [Required]
            public DateTime CreatedOn { get; set; }

            [Required]
            public bool IsDeleted { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new User
                {
	                UserName = Input.Email,
	                Email = Input.Email,
	                Nickname = Input.Nickname,
	                CreatedOn = DateTime.UtcNow,
	                IsDeleted = false
                };

				var areUsersCreated = _userManager.Users.Any();
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    
                    if (!areUsersCreated)
                    {
                       await _userManager.AddToRoleAsync(user, RoleType.Administrator);
                        await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, RoleType.Administrator));
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, RoleType.Member);
                        await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, RoleType.Member));
                    }

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, userCode = code },
                        protocol: Request.Scheme);
	                callbackUrl = callbackUrl.Replace("amp;", "");

					await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by {callbackUrl}");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
