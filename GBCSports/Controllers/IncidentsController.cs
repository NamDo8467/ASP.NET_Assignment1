﻿using GBCSports.Data;
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
            
            IEnumerable<Incidents> incidents = _db.Incidents.ToList();
            return View(incidents);
        }
        
        public IActionResult Add()
        {
            Incidents incident = new Incidents();
            ViewBag.ButtonName = "Add Incident";
            ViewBag.Action = "Add";
            return View("Add", incident);
        }

        [HttpPost]
        public IActionResult Add(Incidents incident)
        {
            if (!ModelState.IsValid)
            {
                Incidents incidentObj = new Incidents();
                ViewBag.ButtonName = "Add Incident";
                ViewBag.Action = "Add";
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
        public IActionResult Edit(Incidents incident)
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
    }
}
