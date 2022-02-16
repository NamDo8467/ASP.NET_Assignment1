using GBCSports.Models;
using Microsoft.AspNetCore.Mvc;

namespace GBCSports.Controllers
{
    public class ProductController : Controller
    {
        private ProductContext context { get; set; }
        public ProductController(ProductContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            var product = context.Products.OrderBy(c => c.Release_Date).ToList();
            return View(product);
        }

        [HttpGet]
        public IActionResult GotoAdd()
        {
            return View("Add");
        }
        [HttpPost]
        public IActionResult Add(Product good)
        {
            if (ModelState.IsValid)
            {
                context.Products.Add(good);
                context.SaveChanges();
                return RedirectToAction("Index", "Product");
            }
            return View(good);
        }

        [HttpGet]
        public IActionResult Edit(int id)
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
                return RedirectToAction("Index", "Product");
            }
            return View("Edit",good);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {

            
            ViewBag.Id = id;
            var product = context.Products.FirstOrDefault(c => c.Id == id);
            ViewBag.Name = product.Name;
            return View(product);
        }
        [HttpPost]
        public IActionResult Delete1(int id)
        {

            var good = context.Products.FirstOrDefault(c => c.Id == id);
            if (good == null)
            {
                
                return NotFound();
            }
            context.Products.Remove(good);
            context.SaveChanges();
            return RedirectToAction("Index", "Product");

        }
    }
}
