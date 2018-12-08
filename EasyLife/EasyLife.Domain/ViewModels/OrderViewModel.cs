using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using EasyLife.Domain.Models;

namespace EasyLife.Domain.ViewModels
{
	public class OrderViewModel
	{
		public int Id { get; set; }

		[Required]
		public string ServiceType { get; set; }

		[Required]
		public string Address { get; set; }

		public string Status { get; set; }

		[Required]
		public string ClientEmail { get; set; }
	}
}
