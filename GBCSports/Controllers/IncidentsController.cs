using GBCSports.Data;
using GBCSports.Models;
using Microsoft.AspNetCore.Mvc;

namespace GBCSports.Controllers
{
    public class IncidentsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public IncidentsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {

            IEnumerable<Incident> incidents = _db.Incidents.ToList();
            return View(incidents);
        }

        public IActionResult Add()
        {
            Incident incident = new Incident();
            ViewBag.ButtonName = "Add Incident";
            ViewBag.Action = "Add";
            ViewBag.Customers = _db.Customers.ToList();
         
            return View("Add", incident);
        }

        [HttpPost]
        public IActionResult Add(Incident incident)
        {
            if (!ModelState.IsValid)
            {
                Incident incidentObj = new Incident();
                ViewBag.ButtonName = "Add Incident";
                ViewBag.Action = "Add";
                ViewBag.Customers = _db.Customers.ToList();
                return View("Add", incidentObj);
            }

            _db.Incidents.Add(incident);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            ViewBag.ButtonName = "Update Incident";
            ViewBag.Action = "Edit";
            var incident = _db.Incidents.Find(id);
            if (incident == null)
            {
                return NotFound();
            }


            return View("Add", incident);
        }

        [HttpPost]
        public IActionResult Edit(Incident incident)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ButtonName = "Update Incident";
                ViewBag.Action = "Edit";
                return View("Add", incident);
            }
            _db.Incidents.Update(incident);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            ViewBag.DeleteId = id;
            return View("Delete");
        }

        [HttpPost]
        public IActionResult DeleteIncident(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
           var incident =  _db.Incidents.Find(id);
            if (incident == null) {
                return NotFound();
            }
            _db.Incidents.Remove(incident);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
