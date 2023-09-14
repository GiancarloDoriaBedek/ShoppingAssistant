using Microsoft.EntityFrameworkCore;
using ShoppingAssistant.Models;

namespace ShoppingAssistant.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CustomRole> CustomRoles { get; set; }
    }
}
