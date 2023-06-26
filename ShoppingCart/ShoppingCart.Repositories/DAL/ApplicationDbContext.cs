using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models.Entities;

namespace ShoppingCart.Repositories.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<CartItem> Cart { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=shopping-cart-local;Trusted_Connection=True;MultipleActiveResultSets=true;Connection Timeout=30;");
        }
    }
}
