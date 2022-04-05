using GBCSports.Data;
using GBCSports.Models;
//using GBCSports.ViewModels;
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

        [Route("/incidents")]
        public IActionResult List()
        {

            IncidentManagerViewModel incidentViewModel = new IncidentManagerViewModel();
            incidentViewModel.IncidentList = new List<Incident>();
            TempData["Incident"] = "text-white";
            incidentViewModel.Filter = HttpContext.Request.Query["Filter"].ToString();

            if (incidentViewModel.Filter == "unassigned")
            {
                TempData["unassigned"] = "active";
                incidentViewModel.IncidentList = _db.Incidents.Where(incident => incident.Technician == null).ToList();
            }
            else if (incidentViewModel.Filter == "open")
            {
                TempData["open"] = "active";
                incidentViewModel.IncidentList = _db.Incidents.Where(incident => incident.DateClosed == null).ToList();
            }
            else
                
            {
                TempData["all"] = "active";
                incidentViewModel.IncidentList = _db.Incidents.ToList();
            }
            
            return View("Index", incidentViewModel);
        }

        private void ConfigureViewModel(IncidentViewModel incidentViewModel)
        {
            
            incidentViewModel.CustomerList = new List<string>();
            incidentViewModel.CustomerList = _db.Customers.Select(customer => customer.FirstName + " " + customer.LastName).ToList();

            incidentViewModel.ProductList = new List<string>();
            incidentViewModel.ProductList = _db.Products.Select(product => product.Name).ToList();

            incidentViewModel.TechnicianList = new List<string>();
            incidentViewModel.TechnicianList = _db.Technicians.Select(technician => technician.FirstName + " " + technician.LastName).ToList();
        }

        public IActionResult Add()
        {
            IncidentViewModel incidentViewModel = new IncidentViewModel();
            //Incident incident = new Incident();

            ConfigureViewModel(incidentViewModel);

            return View(incidentViewModel);
        }

        [HttpPost]
        public IActionResult Add(IncidentViewModel incident)
        {
            
            if (!ModelState.IsValid)
            {
                
                //TempData["m"] = incident.CustomerName;
                ConfigureViewModel(incident);
                return View(incident);
            }
            Incident i = new Incident()
            {
                CustomerName = incident.CustomerName,
                Product = incident.Product,
                Title = incident.Title,
                Description = incident.Description,

                Technician = incident.Technician,
                DateOpened = incident.DateOpened,
                DateClosed = incident.DateClosed,

            };

            _db.Incidents.Add(i);
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
            
            
            if (incident == null)
            {
                return NotFound();
            }
            
            IncidentViewModel incidentViewModel = new IncidentViewModel();
            
            incidentViewModel.CustomerName = incident.CustomerName;
            incidentViewModel.Product = incident.Product;   
            incidentViewModel.Title = incident.Title;
            incidentViewModel.Description = incident.Description;
            incidentViewModel.Technician = incident.Technician;
            incidentViewModel.DateOpened = incident.DateOpened;
            incidentViewModel.DateClosed = incident.DateClosed;
            ConfigureViewModel(incidentViewModel);

            TempData["t"] = incidentViewModel.CustomerName;


            return View("Edit", incidentViewModel);
        }

        [HttpPost]
        public IActionResult Edit(Incident incident)
        {
            IncidentViewModel incidentViewModel = new IncidentViewModel();
            if (!ModelState.IsValid)
            {
                ConfigureViewModel(incidentViewModel);
                incidentViewModel.CustomerName = incident.CustomerName;
                incidentViewModel.Product = incident.Product;
                incidentViewModel.Title = incident.Title;
                incidentViewModel.Description = incident.Description;
                incidentViewModel.Technician = incident.Technician;
                incidentViewModel.DateOpened = incident.DateOpened;
                incidentViewModel.DateClosed = incident.DateClosed;

                return View("Edit", incidentViewModel);
            }

            _db.Incidents.Update(incident);
            _db.SaveChanges();
            return RedirectToAction("List", incidentViewModel);
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
