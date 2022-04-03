//using GBCSports.Data;
using GBCSports.Models;

namespace GBCSports.ViewModels
{
    public class IncidentViewModel
    {
        public List<Incident> incientList { get; set; }
        public string filter { get; set; }
        
        public List<Customer> customerList { get; set; }

               
    }
}
