using System.ComponentModel.DataAnnotations;

namespace GBCSports.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; } 

        [Required(ErrorMessage ="Please provide Code", AllowEmptyStrings = false)]
        public string Code { get; set; }



        [Required(ErrorMessage ="Please provide Name", AllowEmptyStrings = false)]        
        public string Name { get; set; }


        [Required(ErrorMessage = "Please provide Price")]
        
        public float? Price { get; set; }


        [Required(ErrorMessage = "Please provide Date")]
        public DateTime? Release_Date { get; set; } = DateTime.Now;
    }
}