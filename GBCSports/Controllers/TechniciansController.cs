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
            IEnumerable<Technicians> technicianList = db.Technicians;
            return View(technicianList);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Technicians obj)
        {
            db.Technicians.Add(obj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var technicianFromDb = db.Technicians.Find(id);

            if(technicianFromDb == null)
            {
                return NotFound();
            }

            return View(technicianFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Technicians obj)
        {
            db.Technicians.Update(obj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var technicianFromDb = db.Technicians.Find(id);

            if (technicianFromDb == null)
            {
                return NotFound();
            }

            return View(technicianFromDb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = db.Technicians.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            db.Technicians.Remove(obj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
