using GBCSports.Data;
using GBCSports.Models;
using GBCSports.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GBCSports.Controllers
{
    public class RegistrationController : Controller
    {
        private ApplicationDbContext db { get; set; }
        public RegistrationController(ApplicationDbContext ctx)
        {
            db = ctx;
        }


        [Route("/registration/getcustomer")]
        public IActionResult List()
        {
            var vm = new RegistrationGetCustomerViewModel();
            vm.CustomerList = new List<string>();
            vm.CustomerList = db.Customers.Select(customer => customer.FirstName + " " + customer.LastName).ToList();

            vm.CustomerIdList = new List<int>();
            vm.CustomerIdList = db.Customers.Select(customer => customer.Id).ToList();

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
            if (HttpContext.Request.Query["id"].ToString() != "")
            {
                vm.CustomerId = int.Parse(HttpContext.Request.Query["id"].ToString());
            }
            else
            {
                vm.CustomerId = HttpContext.Session.GetInt32("id").Value;
            }
            

            HttpContext.Session.SetInt32("id", vm.CustomerId);

            TempData["customerName"] = db.Customers.Find(HttpContext.Session.GetInt32("id"))?.FirstName + " " + db.Customers.Find(HttpContext.Session.GetInt32("id"))?.LastName;



            vm.CustomerList = new List<string>();


            vm.CustomerIdList = new List<int>();


            vm.RegisteredProductList = new List<Product>();
            var dataList = db.RegisterProducts.Where(d => d.CustomerId == vm.CustomerId).Select(r => r.ProductId).ToList();

            dataList.ForEach(data =>
            {
                db.Products.Where(product => product.Id == data).ToList().ForEach(product =>
                {
                    vm.RegisteredProductList.Add(product);
                });
            });


            vm.ProductList = new List<Product>();

            var registeredProductIdList = new List<int>();
            foreach (var product in vm.RegisteredProductList)
            {
                registeredProductIdList.Add(product.Id);
            }

            vm.ProductList = db.Products.Where(product => registeredProductIdList.Contains(product.Id) == false).ToList();


            return View("RegisterCustomer", vm);
        }


        [HttpPost]
        public RedirectToActionResult RegisterProductToCustomer(RegistrationGetCustomerViewModel vm)
        {
            RegisterProduct registerProduct = new RegisterProduct();
            registerProduct.ProductId = vm.RegisteredProductId;
            registerProduct.CustomerId = vm.CustomerId;
            db.RegisterProducts.Add(registerProduct);
            TempData["action"] = "Registered successfully";
            TempData["style"] = "operation-message bg-success w-100 text-white fw-bold d-flex align-items-center justify-content-center fs-2 my-3";
            TempData["height"] = "height: 100px;";
            db.SaveChanges();
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


            var product = db.RegisterProducts.Where(product => product.ProductId == productId).Where(product => product.CustomerId == customerId).FirstOrDefault();
            if (product == null)
            {
                return RedirectToAction("Error");
            }

            TempData["action"] = "Unregistered successfully";
            TempData["style"] = "operation-message bg-success w-100 text-white fw-bold d-flex align-items-center justify-content-center fs-2 my-3";
            TempData["height"] = "height: 100px;";
            db.RegisterProducts.Remove(product);
            db.SaveChanges();

            return RedirectToAction("RegisterCustomer");
        }

        public IActionResult Error()
        {
            return View("Error");
        }


    }
}