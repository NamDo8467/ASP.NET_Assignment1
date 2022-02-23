using GBCSports.Models;
using Microsoft.EntityFrameworkCore;

namespace GBCSports.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Technician> Technicians { get; set; }

        public DbSet<Product> Products { get; set; }    
    }
}
