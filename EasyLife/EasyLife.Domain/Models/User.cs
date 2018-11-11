using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyLife.Domain.Models
{
    public class User : IdentityUser
    {
        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}
