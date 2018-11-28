using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EasyLife.Domain.Models
{
	public class Donator
	{
		public int Id { get; set; }

		public int UserId { get; set; }
		public User User { get; set; }

		[Required]
		public decimal AmountDonated { get; set; }

		[Required]
		public bool IsDeleted { get; set; }

		[Required]
		public DateTime CreatedOn { get; set; }
	}
}
