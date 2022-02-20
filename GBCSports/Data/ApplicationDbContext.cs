using GBCSports.Models;
using Microsoft.EntityFrameworkCore;

namespace GBCSports.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Technicians> Technicians { get; set; }
    }
}
