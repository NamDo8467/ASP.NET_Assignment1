using System.ComponentModel.DataAnnotations;

namespace GBCSports.Models
{
    public class Customer
    {
        const string errorMessage = "Can  not leave this field empty";

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = errorMessage)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } 
        
        [Required(ErrorMessage = errorMessage)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Required(ErrorMessage = errorMessage)]
        public string Address { get; set; }


        [Required(ErrorMessage = errorMessage)]
        public string City { get; set; }


        [Required(ErrorMessage = errorMessage)]
        public string State { get; set; }


        [Required(ErrorMessage = errorMessage)]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = errorMessage)]
        public string Country { get; set; }
        
        public string? Email { get; set; }

        public string? Phone { get; set; }
    }
}
