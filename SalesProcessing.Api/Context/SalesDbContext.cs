using Microsoft.EntityFrameworkCore;

namespace SalesProcessing.Api.Context
{
    public class SalesDbContext : DbContext
    {
        public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options)
        {
        }

        public DbSet<DailySales> DailySales { get; set; }
        public DbSet<WeeklySales> WeeklySales { get; set; }
    }
}
