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
        public IActionResult Index()
        {
            IEnumerable<Customer> customerList = db.Customers.ToList();
            
            return View(customerList);
        }
        public IActionResult Add()
        {
            ViewBag.CountryList = db.Countries.ToList();

            return View();
        }
    }


}
