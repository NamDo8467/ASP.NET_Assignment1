using System.ComponentModel.DataAnnotations;

namespace GBCSports.Models
{
    public class Technician
    {
        [Key]
        public int TechnicianId { get; set; }

        [Required]
        public string TechnicianName { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }  
    }
}
