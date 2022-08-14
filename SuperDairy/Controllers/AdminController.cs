using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SuperDairy.Models.Admin;
using SuperDairy.Models;
using System.Web;
using System.Security.Claims;
using Core.Model;
namespace SuperDairy.Controllers
{
    [Authorize(Policy = "AdminOnly")]

    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            int UserCount= Core.Model.User.GetUserCount(Common.ConnectionString, (int)Core.UserRole.SUPPLIER);
            ViewData["UserCount"] = UserCount;

            return View();
        }

        public ActionResult CollectMilk()
        {
            List<Core.Model.User> users = Core.Model.User.GetUsers(Common.ConnectionString, (int)Core.UserRole.SUPPLIER).ToList();
            ViewData["Users"] = users;
            List<Rate> rates = Rate.GetRates(null,Common.ConnectionString);
            ViewData["Rates"] =rates;
            return View();
        }
        [HttpPost]
        public ActionResult CollectMilk(CollectMilkModelView milk)
        {
            int userId = Int32.Parse(User.Claims.First(u => u.Type == ClaimTypes.NameIdentifier)?.Value ?? "0");
            MilkInventory inventory = new MilkInventory(milk.UserId, milk.Date, milk.Batch,milk.MilkType, milk.Fat, milk.Price, milk.Quantity, milk.Amount, milk.Comment, milk.Status, DateTime.Now, userId, DateTime.Now, userId);
            inventory.Save(Common.ConnectionString, isNew :true);
            return RedirectToAction(nameof(CollectMilk));
        }
        public ActionResult Bill()
        {
            List<Core.Model.User> users = Core.Model.User.GetUsers(Common.ConnectionString, (int)Core.UserRole.SUPPLIER).ToList();
            ViewData["Users"] = users;
            return View();
        }
        [HttpPost]
        public ActionResult GetBill(int userId, DateTime date)
        {
            var nextDate = date.AddDays(10);
            List<MilkInventory> list=MilkInventory.GetInventoryList(userId, date,nextDate, Common.ConnectionString);
            ViewData["InventoryList"] = list;
            ViewData["TotalMilk"] = list.Sum(e => e.Quantity);
            ViewData["TotalAmount"] = list.Sum(e => e.Amount);
            return View();
        }
    }
}
