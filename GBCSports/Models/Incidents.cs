using System.ComponentModel.DataAnnotations;

namespace GBCSports.Models
{
    public class Incident
    {
        public const string errorMessage = "Can not leave this field empty";

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = errorMessage)]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = errorMessage)]
        public string Product { get; set; }

        [Required(ErrorMessage = errorMessage)]
        public string Title { get; set; }

        [Required(ErrorMessage = errorMessage)]
        public string Description { get; set; }

        public string? Technician { get; set; }
        public DateTime? DateOpened { get; set; } = DateTime.Now;

        [Required(ErrorMessage = errorMessage)]
        public DateTime DateClosed { get; set; } 
    }
}
