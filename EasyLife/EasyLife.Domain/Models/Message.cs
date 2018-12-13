using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EasyLife.Domain.Models
{
	public class Message
	{
		public int Id { get; set; }
		
		[Required]
		public string Content { get; set; }

		[Required]
		public DateTime CreatedOn { get; set; }

		[Required]
		public string SenderId { get; set; }
		public virtual User Sender { get; set; }

		[Required]
		public string ReceiverEmail { get; set; }
	}
}
