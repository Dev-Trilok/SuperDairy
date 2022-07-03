using Microsoft.AspNetCore.Mvc;

namespace SuperDairy.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
