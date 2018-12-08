using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EasyLife.Domain.Models
{
	public class Order
	{
		public int Id { get; set; }

		[Required]
		public string ServiceType { get; set; }

		[Required]
		public string Address { get; set; }

		[Required]
		public string Status { get; set; }

		[Required]
		public string ClientId { get; set; }
		public virtual User Client { get; set; }

		[Required]
		public DateTime CreatedOn { get; set; }
	}
}
