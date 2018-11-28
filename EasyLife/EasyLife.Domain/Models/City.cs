using System;
using System.Collections.Generic;
using System.Text;

namespace EasyLife.Domain.Models
{
	public class City
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int OfficeId { get; set; }
		public Office Office { get; set; }
	}
}
