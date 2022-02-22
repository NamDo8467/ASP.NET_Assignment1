using System.ComponentModel.DataAnnotations;

namespace GBCSports.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
