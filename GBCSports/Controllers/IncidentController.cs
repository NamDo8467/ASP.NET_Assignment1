using GBCSports.Data;
using GBCSports.Models;
using Microsoft.AspNetCore.Mvc;

namespace GBCSports.Controllers
{
    public class IncidentController : Controller
    {
        private readonly ApplicationDbContext _db;

        public IncidentController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult List()
        {

            IEnumerable<Incident> incidents = _db.Incidents;
            return View("Index", incidents);
        }

        public IActionResult Add()
        {
            Incident incident = new Incident();
            ViewBag.Customers = _db.Customers.ToList();
            ViewBag.Products = _db.Products.ToList();
            ViewBag.Technicians = _db.Technicians.ToList();
            
            return View("Add", incident);
        }

        [HttpPost]
        public IActionResult Add(Incident incident)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Customers = _db.Customers.ToList();
                ViewBag.Products = _db.Products.ToList();
                ViewBag.Technicians = _db.Technicians.ToList();
                return View(incident);
            }

            _db.Incidents.Add(incident);
            _db.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var incident = _db.Incidents.Find(id);
            ViewBag.Customers = _db.Customers.ToList();
            ViewBag.Products = _db.Products.ToList();
            ViewBag.Technicians = _db.Technicians.ToList();
            if (incident == null)
            {
                return NotFound();
            }


            return View("Edit", incident);
        }

        [HttpPost]
        public IActionResult Edit(Incident incident)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Customers = _db.Customers.ToList();
                ViewBag.Products = _db.Products.ToList();
                ViewBag.Technicians = _db.Technicians.ToList();
               
                return View("Edit", incident);
            }

            _db.Incidents.Update(incident);
            _db.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var incident = _db.Incidents.Find(id);
            if(incident == null)
            {
                return NotFound();
            }

            ViewBag.DeleteId = id;
            return View(incident);
        }

        [HttpPost]
        public IActionResult DeleteIncident(int? id)
        {
           
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var incident = _db.Incidents.Find(id);
            if(incident == null)
            {
                return NotFound();
            }
            _db.Incidents.Remove(incident);
            _db.SaveChanges();
            return RedirectToAction("List");

        }
    }
}
