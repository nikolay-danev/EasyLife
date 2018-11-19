using EasyLife.Domain.GlobalConstatns;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EasyLife.Domain.Models
{
	public class Service
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Service title is required.")]
		[StringLength(60, ErrorMessage = "Service title should be less than 60 characters.")]
		public string ServiceTitle { get; set; }

		[Required(ErrorMessage = "Description is required.")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Main image is required.")]
		[NotMapped]
		public IFormFile MainImage { get; set; }

		[NotMapped]
		public ICollection<IFormFile> Images { get; set; }

		[Required]
		public DateTime CreatedOn { get; set; }

		[Required(ErrorMessage = "Service type is required.")]
		public string ServiceType { get; set; }
	}
}
