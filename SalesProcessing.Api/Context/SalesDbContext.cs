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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DailySales>().Property(sale => sale.Amount).HasPrecision(18, 2);
            modelBuilder.Entity<WeeklySales>().Property(sale => sale.Amount).HasPrecision(18, 2);
        }
    }
}
