using Microsoft.AspNetCore.Mvc;
using SuperDairy.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Core.Model;

namespace SuperDairy.Controllers
{ 
    [Authorize("LoggedIn")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            if (User.FindFirstValue(ClaimTypes.System) != null)
                return RedirectToAction("Index", "Admin");
            return View();
        }

        public ActionResult GetHistory(DateTime date)
        {
            int userId = Int32.Parse(User.Claims.Where(u => u.Type == ClaimTypes.NameIdentifier).First()?.Value ?? "0");
            List<MilkInventory> list = MilkInventory.GetInventoryList(userId,date,SuperDairy.Models.Common.ConnectionString);
            ViewData["Inventories"] = list;
            ViewData["Date"] = date;
            return View();
        }

        public ActionResult GetBill()
        {
            int userId = Int32.Parse(User.Claims.Where(u => u.Type == ClaimTypes.NameIdentifier).First()?.Value ?? "0");
            List<Bill> bills = Bill.GetBills(userId, Common.ConnectionString);
            ViewData["Bills"]= bills;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}