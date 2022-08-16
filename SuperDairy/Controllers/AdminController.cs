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
            var user = new Core.Model.User(userId, Common.ConnectionString);
            int modifier = Int32.Parse(@User.Claims.Where(E => E.Type == ClaimTypes.NameIdentifier).First()?.Value);
            ViewData["InventoryList"] = list;
            var totalMilk= list.Sum(e => e.Quantity);
            ViewData["TotalMilk"] = totalMilk;
            var totalAmount= list.Sum(e => e.Amount); 
            ViewData["TotalAmount"] = totalAmount;
            ViewData["StartDate"] = date;
            ViewData["EndDate"] = nextDate;
            ViewData["UserId"] = userId;
            ViewData["UserName"] = user.Name;
            Bill bill = new Bill(userId, date, nextDate, Common.ConnectionString);
            if (bill.Id == Guid.Empty)
            {
                bill = new Bill(userId, date, nextDate, totalMilk, totalAmount, false, DateTime.Now, DateTime.Now, modifier, modifier);
                bill.Save(Common.ConnectionString);
            }
            ViewData["BillId"] = bill.Id;
            ViewData["IsPaid"] = bill.IsPaid;
            return View();
        }
        [HttpPost]
        public ActionResult MarkBillPaid(Guid guid)
        {
            Bill bill = new Bill(guid, Common.ConnectionString);
            bill.IsPaid= true;
            bill.LastModified=DateTime.Now;
            bill.LastModifiedBy = Int32.Parse(@User.Claims.Where(E => E.Type == ClaimTypes.NameIdentifier).First()?.Value);
            return new JsonResult(new { result=bill.Save(Common.ConnectionString)});
        }
    }
}
