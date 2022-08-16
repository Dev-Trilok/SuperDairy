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
            bills.RemoveAll(e => e.TotalAmount == 0);
            ViewData["Bills"]= bills;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public ActionResult ShowBill(Guid id)
        {
            Bill bill = new Bill(id, Common.ConnectionString);
            List<MilkInventory> list=MilkInventory.GetInventoryList(bill.UserId, bill.StartDate,bill.EndDate, Common.ConnectionString);
            ViewData["InventoryList"] = list;
            var totalMilk= list.Sum(e => e.Quantity);
            ViewData["TotalMilk"] = totalMilk;
            var totalAmount= list.Sum(e => e.Amount); 
            ViewData["TotalAmount"] = totalAmount;
            ViewData["StartDate"] = bill.StartDate;
            ViewData["EndDate"] = bill.EndDate;
            ViewData["IsPaid"] = bill.IsPaid;
            return View();
        }
    }
}