using Microsoft.AspNetCore.Mvc;

namespace GBCSports.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult List()
        {
            return View("Index");
        }
    }
}
