using GBCSports.Data;
using GBCSports.Models;
using Microsoft.AspNetCore.Mvc;

namespace GBCSports.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext context { get; set; }
        public ProductController(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        [Route("/products")]
        public ViewResult List()
        {
            var product = context.Products.OrderBy(c => c.Release_Date).ToList();
            return View(product);
        }
        private void ValidateFields(Product good)
        {
            if (good.Code == null)
            {
                TempData[Convert.ToString(1)] = "background-color: #FECBA1; border-color:red;";
            }

            if (good.Name == null)
            {
                TempData[Convert.ToString(2)] = "background-color: #FECBA1; border-color:red;";
            }

            if (good.Price == null || !double.TryParse(good.Price.ToString(), out double price))
            {
                TempData[Convert.ToString(3)] = "background-color: #FECBA1; border-color:red;";
            }

            if (good.Release_Date == null)
            {
                TempData[Convert.ToString(4)] = "background-color: #FECBA1; border-color:red;";
            }
        }

        [HttpGet]
        public ViewResult Add()
        {
            return View();
        }
        [HttpPost]
        [Route("/add")]
        public IActionResult AddToDatabase(Product good)
        {
            
            if (ModelState.IsValid)
            {

               
                context.Products.Add(good);
                context.SaveChanges();
                TempData["action"] = " was added";
                TempData["productName"] = good.Name;
                TempData["style"] = "operation-message bg-success w-100 text-white fw-bold d-flex align-items-center justify-content-center fs-2 my-3";
                TempData["height"] = "height: 100px;";
                return RedirectToAction("List");
            }
            ValidateFields(good);
            return View("Add", good);
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            var product = context.Products.FirstOrDefault(c => c.Id == id);
            return View(product);
        }
       

        [HttpPost]
        public IActionResult Edit(Product good)
        {
            if (ModelState.IsValid) 
            {
                context.Products.Update(good);
                context.SaveChanges();
                TempData["action"] = "Edit successfully";
                TempData["style"] = "operation-message bg-success w-100 text-white fw-bold d-flex align-items-center justify-content-center fs-2 my-3";
                TempData["height"] = "height: 100px;";
                return RedirectToAction("List");
            }
            TempData["d"] = "error";
            ValidateFields(good);
            return View("Edit",good);
        }
        [HttpGet]
        public ViewResult Delete(int id)
        {

            
            ViewBag.Id = id;
            var product = context.Products.FirstOrDefault(c => c.Id == id);
            if (product == null) return View("Error");
            ViewBag.Name = product.Name;
            return View(product);
        }
        [HttpPost]
        public RedirectToActionResult Delete1(int id)
        {

            var good = context.Products.FirstOrDefault(c => c.Id == id);
            if (good == null)
            {
                
                return RedirectToAction("Error");
            }
            context.Products.Remove(good);
            context.SaveChanges();
            TempData["action"] = "Deleted successfully";
            TempData["style"] = "operation-message bg-success w-100 text-white fw-bold d-flex align-items-center justify-content-center fs-2 my-3";
            TempData["height"] = "height: 100px;";
            return RedirectToAction("List");

        }
    }
}
