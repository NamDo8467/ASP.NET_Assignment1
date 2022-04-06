using GBCSports.Data;
using GBCSports.Models;
using GBCSports.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GBCSports.Controllers
{
    public class RegistrationController : Controller
    {
        private ApplicationDbContext context { get; set; }
        public RegistrationController(ApplicationDbContext ctx)
        {
            context = ctx;
        }


        [Route("/registration/getcustomer")]
        public IActionResult List()
        {
            var vm = new RegistrationGetCustomerViewModel();
            vm.CustomerList = new List<string>();
            vm.CustomerList = context.Customers.Select(customer => customer.FirstName + " " + customer.LastName).ToList();  

            vm.CustomerIdList = new List<int>();
            vm.CustomerIdList = context.Customers.Select(customer => customer.Id).ToList(); 

            vm.RegisteredProductList = new List<Product>();
          
            return View("Index", vm);
        }

        public void RedirectToProductRegistration(RegistrationGetCustomerViewModel vm)
        {
            Response.Redirect("/registration?id=" + vm.CustomerId, true);

            
        }

        [HttpGet]
        [Route("/registration")]
        public IActionResult RegisterCustomer()
        {
            
            RegistrationGetCustomerViewModel vm = new RegistrationGetCustomerViewModel();
            vm.CustomerId = int.Parse(HttpContext.Request.Query["id"].ToString());
            
            HttpContext.Session.SetInt32("id", vm.CustomerId);
            
            TempData["customerName"] = context.Customers.Find(HttpContext.Session.GetInt32("id"))?.FirstName + " " + context.Customers.Find(HttpContext.Session.GetInt32("id"))?.LastName;
            
            

            vm.CustomerList = new List<string>();
            

            vm.CustomerIdList = new List<int>();
            

            vm.RegisteredProductList = new List<Product>();
            var dataList = context.RegisterProducts.Where(d => d.CustomerId == vm.CustomerId).Select(r => r.ProductId).ToList();

            dataList.ForEach(data =>
            {
                context.Products.Where(product => product.Id == data).ToList().ForEach(product =>
                {
                    vm.RegisteredProductList.Add(product);
                });
            });


            vm.ProductList = new List<Product>();

            var registeredProductIdList = new List<int>();
            foreach(var product in vm.RegisteredProductList)
            {
                registeredProductIdList.Add(product.Id);
            }

            vm.ProductList = context.Products.Where(product=>registeredProductIdList.Contains(product.Id) == false).ToList();


            return View("RegisterCustomer", vm); 
        }


        [HttpPost]
        public RedirectToActionResult RegisterProductToCustomer(RegistrationGetCustomerViewModel vm)
        {
            RegisterProduct registerProduct = new RegisterProduct();
            registerProduct.ProductId = vm.RegisteredProductId;
            registerProduct.CustomerId = vm.CustomerId;
            context.RegisterProducts.Add(registerProduct);

            context.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        [Route("delete")]
        public IActionResult GetDeleteView()

        {
            string id = HttpContext.Request.Query["productId"].ToString();
            ViewBag.ProductId = id;
            

            return View("Delete");
        }


        [HttpPost]
        public RedirectToActionResult DeleteRegistration(int productId)
        {
            int customerId = (int)HttpContext.Session.GetInt32("id");
            
            
            var product = context.RegisterProducts.Where(product => product.ProductId == productId).Where(product => product.CustomerId == customerId).FirstOrDefault();
            if(product == null)
            {
               return RedirectToAction("Error");
            }
            context.RegisterProducts.Remove(product);
            context.SaveChanges();

            return RedirectToAction("List");        
        }

        public IActionResult Error()
        {
            return View("Error");
        }


    }
}
