using GBCSports.Data;
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
        public IActionResult List()
        {
            var product = context.Products.OrderBy(c => c.Release_Date).ToList();
            return View(product);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddToDatabase(Product good)
        {
            
            if (ModelState.IsValid)
            {
               

                context.Products.Add(good);
                context.SaveChanges();
                return RedirectToAction("List");
            }
            return View("Add", good);
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
                return RedirectToAction("List");
            }
            return View("Edit",good);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {

            
            ViewBag.Id = id;
            var product = context.Products.FirstOrDefault(c => c.Id == id);
            if (product == null) return NotFound();
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
            return RedirectToAction("List");

        }
    }
}
