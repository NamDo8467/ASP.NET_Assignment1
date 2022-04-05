using System.ComponentModel.DataAnnotations;

namespace GBCSports.Models
{
    public class IncidentViewModel
    {
        public const string errorMessage = "Can not leave this field empty";
        //public Incident CurrentIncident { get; set; }
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

        [Required(ErrorMessage = errorMessage)]
        public DateTime? DateOpened { get; set; } = DateTime.Now;

        public DateTime? DateClosed { get; set; }


        
       
        public List<string> CustomerList { get; set; }

        public List<string> ProductList { get; set; }

        public List<string> TechnicianList { get; set; }
               
    }
}
