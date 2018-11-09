using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EasyLife.Persistence.Data
{
    public class EasyLifeDbContext : IdentityDbContext
    {
        public EasyLifeDbContext(DbContextOptions<EasyLifeDbContext> options)
            : base(options)
        {
        }
    }
}
