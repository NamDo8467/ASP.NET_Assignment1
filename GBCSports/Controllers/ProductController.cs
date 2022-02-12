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
        public IActionResult List()
        {
            var products = context.Products.OrderBy(c=>c.Release_Date).ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View("", new Product());
        }
    }
}
