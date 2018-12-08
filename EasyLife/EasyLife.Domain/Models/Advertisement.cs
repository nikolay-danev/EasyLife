using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EasyLife.Domain.Models
{
	public class Advertisement
	{
		public int Id { get; set; }

		public string CreatorId { get; set; }
		public virtual User Creator { get; set; }

		[Required]
		public string Url { get; set; }

		[Required]
		[StringLength(50)]
		public string BusinessName { get; set; }

		[Required]
		public string ImageUrl { get; set; }

		[Required]
		public DateTime ExpirationDate { get; set; }

		[Required]
		public DateTime CreatedOn { get; set; }
	}
}
