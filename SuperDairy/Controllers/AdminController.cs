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
            return View();
        }

        public ActionResult CollectMilk()
        {
            List<Core.Model.User> users = Core.Model.User.GetUsers(Common.ConnectionString, (int)Core.UserRole.SUPPLIER).ToList();
            ViewData["Users"] = users;
            List<Rate> rates = Rate.GetRates(Common.ConnectionString);
            ViewData["Rates"] =rates;
            return View();
        }
        [HttpPost]
        public ActionResult CollectMilk(CollectMilkModelView milk)
        {
            int userId = Int32.Parse(User.Claims.Where(u => u.Type == ClaimTypes.NameIdentifier).First()?.Value ?? "0");
            MilkInventory inventory = new MilkInventory(milk.UserId, milk.Date, milk.Batch,milk.MilkType, milk.Fat, milk.Price, milk.Quantity, milk.Amount, milk.Comment, milk.Status, DateTime.Now, userId, DateTime.Now, userId);
            inventory.Save(Common.ConnectionString, isNew :true);
            return RedirectToAction(nameof(CollectMilk));
        }
    }
}
