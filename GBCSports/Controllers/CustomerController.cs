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

        //[Route("/customers")]
        public IActionResult List()
        {
            TempData["Customer"] = "text-white";
            IEnumerable<Customer> customerList = db.Customers;
            
            return View("Index", customerList);

            
        }

        private void ValidateFields(Customer customer)
        {
            if (customer.FirstName == null)
            {
                TempData[Convert.ToString(1)] = "background-color: #FECBA1; border-color:red;";
            }

            if (customer.LastName == null)
            {
                TempData[Convert.ToString(2)] = "background-color: #FECBA1; border-color:red;";
            }

            if (customer.Address == null)
            {
                TempData[Convert.ToString(3)] = "background-color: #FECBA1; border-color:red;";
            }

            if (customer.City == null)
            {
                TempData[Convert.ToString(4)] = "background-color: #FECBA1; border-color:red;";
            }

            if (customer.State == null)
            {
                TempData[Convert.ToString(5)] = "background-color: #FECBA1; border-color:red;";
            }
            if (customer.PostalCode== null)
            {
                TempData[Convert.ToString(6)] = "background-color: #FECBA1; border-color:red;";
            }
            if (customer.Country == null)
            {
                TempData[Convert.ToString(7)] = "background-color: #FECBA1; border-color:red;";

            }
            if (customer.Email == null)
            {
                TempData[Convert.ToString(8)] = "background-color: #FECBA1; border-color:red;";
            }
            if (db.Customers.FirstOrDefault(c => c.Email == customer.Email) != null)
            {
                ModelState.AddModelError("Email", "Email address already in use");
                TempData[Convert.ToString(8)] = "background-color: #FECBA1; border-color:red;";
            }
            if (customer.Phone == null || customer.Phone != @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")
            {
                TempData[Convert.ToString(9)] = "background-color: #FECBA1; border-color:red;";
            }
        }


        /* Add actions */
        public IActionResult Add()
        {
            
            ViewBag.CountryList = db.Countries.Select(country => country.Name).ToList();
       

            return View();
        }

        [HttpPost]
        public IActionResult Add(Customer customer)
        {
            if (!ModelState.IsValid)
            {

                ValidateFields(customer);
                ViewBag.CountryList = db.Countries.Select(country => country.Name).ToList();

                

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
                ValidateFields(customer);
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
