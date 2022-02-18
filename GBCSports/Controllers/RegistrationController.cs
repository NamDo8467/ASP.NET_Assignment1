using Microsoft.AspNetCore.Mvc;

namespace GBCSports.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}
