using GBCSports.Models;
using Microsoft.EntityFrameworkCore;

namespace GBCSports.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Incident> Incidents { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData
                (
                new Country
                {
                    Id = 1,
                    Name = "Canada"
                },
                new Country
                {
                    Id = 2,
                    Name = "VietNam"
                },
                new Country
                {
                    Id = 3,
                    Name = "United States of America"
                },
                new Country
                {
                    Id = 4,
                    Name = "Russia"
                },
                new Country
                {
                    Id = 5,
                    Name = "Italy"
                },
                new Country
                {
                    Id = 6,
                    Name = "France"
                },
                new Country
                {
                    Id = 7,
                    Name = "Spain"
                },
                new Country
                {
                    Id = 8,
                    Name = "England"
                },
                new Country
                {
                    Id = 9,
                    Name = "Germany"
                },
                new Country
                {
                    Id = 10,
                    Name = "Netherlands"
                },
                new Country
                {
                    Id = 11,
                    Name = "Poland"
                }


                );
        }

    }
}
