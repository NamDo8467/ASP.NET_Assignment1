using GBCSports.Models;
using Microsoft.EntityFrameworkCore;

namespace GBCSports.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RegisterProduct>().HasKey(product => new { product.CustomerId, product.ProductId });
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Technician> Technicians { get; set; }

        public DbSet<Product> Products { get; set; }




        public DbSet<RegisterProduct> RegisterProducts { get; set; }
    }
}
