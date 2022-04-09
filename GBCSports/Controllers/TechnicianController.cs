using GBCSports.Data;
using GBCSports.Models;
using GBCSports.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GBCSports.Controllers
{
    public class TechnicianController : Controller
    {
        private readonly ApplicationDbContext db;
        public TechnicianController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [Route("/technicians")]
        public IActionResult List()
        {
            TempData["Technician"] = "text-white";
            IEnumerable<Technician> technicianList = db.Technicians;
            return View(technicianList);
        }

        public IActionResult Add()
        {
            TempData["Technician"] = "text-white";
            return View();
        }



        private void ValidateFields(Technician technician)
        {
            if (technician.FirstName == null)
            {
                TempData["1"] = "background-color: #FECBA1; border-color:red;";
            }
            if (technician.LastName == null)
            {
                TempData["2"] = "background-color: #FECBA1; border-color:red;";
            }
            if (technician.Email == null)
            {
                TempData["3"] = "";
            }
            else if (db.Technicians.FirstOrDefault(c => c.Email == technician.Email) != null)
            {
                Technician c = db.Technicians.FirstOrDefault(c => c.Email == technician.Email);

                if (technician.FirstName != c.FirstName && technician.LastName != c.LastName && technician.Email == c.Email)
                {

                    ModelState.AddModelError("Email", "Email address already in use");
                    TempData["3"] = "background-color: #FECBA1; border-color:red;";
                }



            }
            if (technician.Phone == null)
            {
                TempData["4"] = "";
            }
            else if (technician.Phone != @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")
            {
                TempData["4"] = "background-color: #FECBA1; border-color:red;";
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Technician obj)
        {
            ValidateFields(obj);
            if (!ModelState.IsValid)
            {
                ValidateFields(obj);
                return View("Add", obj);
            }
            else
            {
                db.Technicians.Add(obj);
                db.SaveChanges();
                TempData["success"] = "Technician added successfully";
                return RedirectToAction("List");

            }
        }

        public IActionResult Edit(int? id)
        {
            TempData["Technician"] = "text-white";
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Technician obj)
        {
            ValidateFields(obj);
            if (!ModelState.IsValid)
            {
                ValidateFields(obj);
                return View(obj);
            }
            db.Technicians.Update(obj);
            db.SaveChanges();
            TempData["success"] = "Technician edited successfully";
            return RedirectToAction("List");
        }

        public IActionResult Delete(int? id)
        {
            TempData["Technician"] = "text-white";
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
            TempData["success"] = "Technician deleted successfully";
            return RedirectToAction("List");
        }

        [Route("/techincident")]
        public IActionResult TechIncident()
        {

            TechIncidentViewModel vm = new TechIncidentViewModel();
            vm.TechnicianList = new List<string>();
            vm.TechnicianList = db.Technicians.Select(t => t.FirstName + " " + t.LastName).ToList();

            vm.TechnicianIdList = new List<int>();
            vm.TechnicianIdList = db.Technicians.Select(t => t.Id).ToList();


            return View(vm);
        }

        public void RedirectToUpdateIncident(TechIncidentViewModel vm)
        {
            Response.Redirect("/techincident/list/" + vm.TechnicianId, true);
        }

        [Route("techincident/list/{id?}")]
        public IActionResult GetIncidentList(int id)
        {


            HttpContext.Session.SetInt32("id", id);

            TechIncidentViewModel vm = new TechIncidentViewModel();
            vm.TechnicianId = id;

            TempData["technician"] = db.Technicians.Find(HttpContext.Session.GetInt32("id"))?.FirstName + " " + db.Technicians.Find(HttpContext.Session.GetInt32("id"))?.LastName;

            vm.TechnicianList = new List<string>();
            vm.TechnicianIdList = new List<int>();

            vm.AssignedIncidentList = new List<Incident>();

            vm.AssignedIncidentList = db.Incidents.Where(i => i.Technician == TempData["technician"]).ToList();

            TempData.Keep("technician");

            return View("IncidentList", vm);

        }

        [Route("/techincident/edit/{id}")]
        public IActionResult EditIncident(int id)
        {
            Incident incident = new Incident();

            incident = db.Incidents.Find(id);
            if (incident == null)
            {
                return NotFound();
            }
            return View("EditIncident", incident);
        }

        [HttpPost]
        public IActionResult EditIncidentPost(Incident incident)
        {
            Incident i = db.Incidents.Find(incident.Id);

            if (i == null)
            {
                return View("Error");
            }

            i.Description = incident.Description;
            i.DateClosed = incident.DateClosed;

            var technicianId = HttpContext.Session.GetInt32("id");

            db.Incidents.Update(i);
            db.SaveChanges();
            return Redirect($"/techincident/list/{technicianId}");
        }


    }
}