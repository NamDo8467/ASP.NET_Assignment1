using GBCSports.Data;
using GBCSports.Models;
using Microsoft.AspNetCore.Mvc;

namespace GBCSports.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext db;
        public CustomerController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult List()
        {
            IEnumerable<Customer> customerList = db.Customers;
            
            return View("Index", customerList);
        }


        /* Add actions */
        public IActionResult Add()
        {
            IEnumerable<Country> countryList = db.Countries;
            ViewBag.CountryList = countryList;

            return View();
        }

        [HttpPost]
        public IActionResult Add(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CountryList = db.Countries.ToList();
                return View(customer);
            }

            db.Customers.Add(customer);
            db.SaveChanges();
            return RedirectToAction("List");
        }



        /* Edit actions*/
        public IActionResult Edit(int id)
        {
            var customer = db.Customers.Find(id);
            if(customer == null)
            {
                return View("Error");
            }
            

            ViewBag.CountryList = db.Countries.ToList();

            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CountryList = db.Countries.ToList();
                return View(customer);
            }

            db.Customers.Update(customer);
            db.SaveChanges();
            return RedirectToAction("List");
        }


        /* Delete actions */
        public IActionResult Delete(int id)
        {
            var customer = db.Customers.Find(id);
            if (customer == null)
            {
                return View("Error");
            }
            ViewBag.DeleteId = id;
            
            return View(customer);
        }

        [HttpPost]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = db.Customers.Find(id);
            if (customer == null)
            {
                return View("Error");
            }

            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("List");
        }
    }


}
