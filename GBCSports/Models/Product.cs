using System.ComponentModel.DataAnnotations;

namespace GBCSports.Models
{
    public class Product
    {
        [Key]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public long Price { get; set; }
        [Required]
        public DateTime Release_Date  { get; set; }
    }
}
