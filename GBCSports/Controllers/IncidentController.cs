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
            ViewData["Title"] = "Incident Manager";
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
        private void ValidateFields(Incident incident)
        {
            if (incident.CustomerName == null)
            {
                TempData[Convert.ToString(1)] = "background-color: #FECBA1; border-color:red;";
            }

            if (incident.Product == null)
            {
                TempData[Convert.ToString(2)] = "background-color: #FECBA1; border-color:red;";
            }

            if (incident.Title == null)
            {
                TempData[Convert.ToString(3)] = "background-color: #FECBA1; border-color:red;";
            }

            if (incident.Description == null)
            {
                TempData[Convert.ToString(4)] = "background-color: #FECBA1; border-color:red;";
            }
            
            if (incident.DateOpened == null)
            {
                TempData[Convert.ToString(5)] = "background-color: #FECBA1; border-color:red;";
            }
            
        }

        public IActionResult Add()
        {
            ViewData["Title"] = "Add Incident";
            IncidentViewModel incidentViewModel = new IncidentViewModel();

            ConfigureViewModel(incidentViewModel);

            return View(incidentViewModel);
        }

        [HttpPost]
        public IActionResult Add(IncidentViewModel incident)
        {
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
            if (!ModelState.IsValid)
            {
                ValidateFields(i);                
                return View(incident);
            }

            TempData["incidentName"] = incident.Title;
            TempData["action"] = " was added successfully";
            TempData["style"] = "operation-message bg-success w-100 text-white fw-bold d-flex align-items-center justify-content-center fs-2 my-3";
            TempData["height"] = "height: 100px;";
            

            _db.Incidents.Add(i);
            _db.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult Edit(int? id)
        {
            ViewData["Title"] = "Edit Incident";
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

           


            return View("Edit", incidentViewModel);
        }

        [HttpPost]
        public IActionResult Edit(IncidentViewModel incident)
        {
            
            
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
            if (!ModelState.IsValid)
            {
                
                ValidateFields(i);
                
         

                return View("Edit", incident);
            }

            TempData["action"] = "Edited successfully";
            TempData["style"] = "operation-message bg-success w-100 text-white fw-bold d-flex align-items-center justify-content-center fs-2 my-3";
            TempData["height"] = "height: 100px;";
            _db.Incidents.Update(i);
            _db.SaveChanges();
            return RedirectToAction("List", incident);
            

        }

        public IActionResult Delete(int? id)
        {
            ViewData["Title"] = "Delete Incident";
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
            TempData["action"] = "Deleted successfully";
            TempData["style"] = "operation-message bg-success w-100 text-white fw-bold d-flex align-items-center justify-content-center fs-2 my-3";
            TempData["height"] = "height: 100px;";
            _db.Incidents.Remove(incident);
            _db.SaveChanges();
            return RedirectToAction("List");

        }
    }
}
