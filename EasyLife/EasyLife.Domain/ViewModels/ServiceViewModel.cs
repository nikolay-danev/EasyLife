using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EasyLife.Domain.ViewModels
{
	public class ServiceViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Service title is required.")]
		[StringLength(60,ErrorMessage = "Service title should be less than 60 characters.")]
		[Display(Name = "Service Title")]
		public string ServiceTitle { get; set; }

		[Required(ErrorMessage = "Description is required.")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Main image is required.")]
		[Display(Name = "Image")]
		public IFormFile MainImage { get; set; }

		[Required(ErrorMessage = "Service type is required.")]
		public string ServiceType { get; set; }
	}
}
