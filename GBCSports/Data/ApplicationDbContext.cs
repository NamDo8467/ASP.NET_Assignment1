using Microsoft.EntityFrameworkCore;


namespace GBCSports.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> good) : base(good) { }

        public DbSet<Product> Products { get; set; }

        

    }
}