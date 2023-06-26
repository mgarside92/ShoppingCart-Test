using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models.Entities;

namespace ShoppingCart.Repositories.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<CartItem> Cart { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
