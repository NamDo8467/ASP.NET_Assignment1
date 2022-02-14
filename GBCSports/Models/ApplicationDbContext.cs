using Microsoft.EntityFrameworkCore;

namespace GBCSports.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Technician> Technicians { get; set; }
    }
}
