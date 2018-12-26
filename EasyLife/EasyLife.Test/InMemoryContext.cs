using System;
using System.Collections.Generic;
using System.Text;
using EasyLife.Domain.Models;
using EasyLife.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace EasyLife.Test
{
	public class InMemoryContext : EasyLifeDbContext
	{
		public InMemoryContext(DbContextOptions<EasyLifeDbContext> options) : base(options)
		{
		}
	}
}
