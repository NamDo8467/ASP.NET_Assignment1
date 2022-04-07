using Microsoft.AspNetCore.Mvc;

namespace GBCSports.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
