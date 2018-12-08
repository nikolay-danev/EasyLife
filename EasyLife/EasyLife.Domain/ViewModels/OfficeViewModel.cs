using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EasyLife.Domain.ViewModels
{
	public class OfficeViewModel
	{
		public int Id { get; set; }

		[Required]
		public string Address { get; set; }

		[Required]
		public string PhoneNumber { get; set; }
	}
}
