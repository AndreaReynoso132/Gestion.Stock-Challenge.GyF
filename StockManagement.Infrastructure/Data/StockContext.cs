using Microsoft.EntityFrameworkCore;
using StockManagement.Core.Entities;

namespace StockManagement.Infrastructure.Data
{
    public class StockContext : DbContext
    {
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public StockContext(DbContextOptions<StockContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>()
                .Property(p => p.FechaCarga)
                .HasConversion(d => d, d => DateTime.SpecifyKind(d, DateTimeKind.Utc));
        }
    }
}
