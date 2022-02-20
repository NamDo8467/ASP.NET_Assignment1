using GBCSports.Data;
using GBCSports.Models;
using Microsoft.AspNetCore.Mvc;

namespace GBCSports.Controllers
{
    public class TechniciansController : Controller
    {
        private ApplicationDbContext db;
        public TechniciansController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Technicians> technicianList = db.Technicians.ToList();
            return View(technicianList);
        }

        [HttpPost]
        public IActionResult Add(TechniciansController technician)
        {
            // db.Technicians.Add(technician);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
