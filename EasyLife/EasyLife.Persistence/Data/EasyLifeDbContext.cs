using Microsoft.EntityFrameworkCore;

namespace EasyLife.Persistence.Data
{
    public class EasyLifeDbContext : DbContext
    {
        public EasyLifeDbContext(DbContextOptions<EasyLifeDbContext> options)
            : base(options)
        {
        }
    }
}
