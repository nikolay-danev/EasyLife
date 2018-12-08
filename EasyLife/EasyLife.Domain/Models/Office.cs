﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EasyLife.Domain.Models
{
	public class Office
	{
		public int Id { get; set; }

		public string Address { get; set; }

		public string PhoneNumber { get; set; }
	}
}
