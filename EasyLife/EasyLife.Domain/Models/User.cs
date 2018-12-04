using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EasyLife.Domain.Models
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(60, ErrorMessage = "Username cannot be more than 60 characters.")]
        public string Nickname { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }

		[Range(0,double.MaxValue)]
	    public double Discount { get; set; }
    }
}
