using Microsoft.EntityFrameworkCore;


namespace GBCSports.Models
{
    public class ProductContext: DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> good) : base(good) {  }

        public DbSet<Product> Products { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Product>().HasData
                (
                new Product
                {
                    Code = "TRNY10",
                    Name = "Tournament Master 1.0",
                    Price = (long)4.99,
                    Release_Date = DateTime.Now
                }
                ) ;
        }
        
    }
}
