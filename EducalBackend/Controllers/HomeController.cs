using Microsoft.AspNetCore.Mvc;

namespace EducalBackend.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
