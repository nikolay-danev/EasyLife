using System;
using System.Collections.Generic;
using System.Text;
using EasyLife.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EasyLife.Persistence.Data
{
    public class EasyLifeDbContext : IdentityDbContext
    {
		public DbSet<Service> Services { get; set; }

		public EasyLifeDbContext(DbContextOptions<EasyLifeDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("Users", "dbo");
        }
    }
}
