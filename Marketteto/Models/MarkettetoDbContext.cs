using Microsoft.EntityFrameworkCore;

namespace Marketteto.Models
{
    public class MarkettetoDbContext : DbContext
    {
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MarkettetoDB;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=True");
        }
    }
}
