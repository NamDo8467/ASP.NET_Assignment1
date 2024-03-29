﻿using System.ComponentModel.DataAnnotations;

namespace GBCSports.Models
{
    public class Technician
    {
        const string errorMessage = "Cannot leave this field empty";

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = errorMessage)]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = errorMessage)]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }
    }
}
