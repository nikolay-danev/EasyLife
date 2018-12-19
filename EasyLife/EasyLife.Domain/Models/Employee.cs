using System;
using System.ComponentModel.DataAnnotations;

namespace EasyLife.Domain.Models
{
	public class Employee
	{
		public int Id { get; set; }

		[Required]
		public string FullName { get; set; }

		[Required]
		public string JobPosition { get; set; }

		[Required]
		public DateTime CreatedOn { get; set; }

		[Required]
		public string ShortDescription { get; set; }

		[Required]
		public string ProfilePictureUrl { get; set; }

		[Required]
		public bool IsFired { get; set; }

		[Required]
		public string FacebookUrl { get; set; }
	}
}
