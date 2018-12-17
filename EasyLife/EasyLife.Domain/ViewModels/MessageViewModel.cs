using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using EasyLife.Domain.Models;

namespace EasyLife.Domain.ViewModels
{
	public class MessageViewModel
	{
		public int Id { get; set; }

		[Required]
		public string Content { get; set; }

		[Required]
		public string SenderEmail { get; set; }

		[Required]
		public string ReceiverEmail { get; set; }

		public DateTime CreatedOn { get; set; }
	}
}
