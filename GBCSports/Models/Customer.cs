using System.ComponentModel.DataAnnotations;


namespace GBCSports.Models
{
    public class Customer
    {
        const string errorMessage = "Can  not leave this field empty";

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = errorMessage)]
        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } 
        
        [Required(ErrorMessage = errorMessage)]
        [StringLength(50, MinimumLength = 1)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Required(ErrorMessage = errorMessage)]
        [StringLength(50, MinimumLength = 1)]
        public string Address { get; set; }


        [Required(ErrorMessage = errorMessage)]
        [StringLength(50, MinimumLength = 1)]
        public string City { get; set; }


        [Required(ErrorMessage = errorMessage)]
        [StringLength(50, MinimumLength = 1)]
        public string State { get; set; }


        [Required(ErrorMessage = errorMessage)]
        [Display(Name = "Postal Code")]
        [StringLength(20, MinimumLength = 1)]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = errorMessage)]
        public string Country { get; set; }

        [StringLength(50, MinimumLength = 0)]
        //[DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Phone number should be in '999-999-9999' format")]
        public string? Phone { get; set; }
    }
}
