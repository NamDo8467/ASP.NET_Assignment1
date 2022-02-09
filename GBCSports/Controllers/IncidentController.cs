using Microsoft.AspNetCore.Mvc;

namespace GBCSports.Controllers
{
    public class IncidentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View("Add");
        }
    }
}
