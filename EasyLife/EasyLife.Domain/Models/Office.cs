using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EasyLife.Domain.Models
{
	public class Office
	{
		public Office()
		{
			this.Cities = new HashSet<City>();
			this.Countries = new HashSet<Country>();
		}
		public int Id { get; set; }

		public virtual ICollection<City> Cities { get; set; }

		public virtual ICollection<Country> Countries { get; set; }
	}
}
