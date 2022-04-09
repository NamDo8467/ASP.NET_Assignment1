using GBCSports.Models;

namespace GBCSports.ViewModels
{
    public class TechIncidentViewModel
    {
        public List<int> TechnicianIdList { get; set; }
        
        public List<string> TechnicianList { get; set; }

        public int TechnicianId { get; set; }

        public List<Incident> AssignedIncidentList { get; set; }
    }
}
