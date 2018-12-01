using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using EasyLife.Domain.Models;

namespace EasyLife.Domain.ViewModels
{
	public class AdvertisementViewModel
	{
		public int Id { get; set; }

		[Required]
		[Display(Name = "Your business website.")]
		public string Url { get; set; }

		[Required]
		[Display(Name = "Your business name.")]
		[StringLength(50)]
		public string BusinessName { get; set; }

		[Required]
		[Display(Name = "Banner for your advertisement.")]
		public IFormFile ImageFile { get; set; }

		[Required]
		public DateTime ExpirationDate { get; set; }

		[Required]
		public DateTime CreatedOn { get; set; }

		public User Creator { get; set; }

		public string ImageUrl { get; set; }
	}
}
