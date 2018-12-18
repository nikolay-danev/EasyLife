using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace EasyLife.Domain.ViewModels
{
	public class EmployeeViewModel
	{
		[Required]
		[Display(Name = "Full name")]
		public string FullName { get; set; }

		[Required]
		[Display(Name = "Job position")]
		public string JobPosition { get; set; }

		[Required]
		[Display(Name = "Short description")]
		public string ShortDescription { get; set; }

		[Required]
		[Display(Name = "Profile picture")]
		public IFormFile ProfilePicture { get; set; }

		public string ProfilePictureUrl { get; set; }

		[Required]
		[Display(Name = "Facebook link")]
		public string FacebookUrl { get; set; }
	}
}
