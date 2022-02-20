using GBCSports.Data;
using GBCSports.Models;
using Microsoft.AspNetCore.Mvc;

namespace GBCSports.Controllers
{
    public class TechnicianController : Controller
    {
        private ApplicationDbContext db;
        public TechnicianController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Technician> technicianList = db.Technicians.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Add(TechnicianController technician)
        {
            // db.Technicians.Add(technician);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
