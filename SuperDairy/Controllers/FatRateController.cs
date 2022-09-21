using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperDairy.Models;
using Microsoft.AspNetCore.Authorization;
namespace SuperDairy.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class FatRateController : Controller
    {
        public FatRateController()
        {
        }
        // GET: FatRate
        public ActionResult Index()
        {
            var buffaloRateList = Core.Model.Rate.GetRates(Core.MilkType.BUFFALO,Common.ConnectionString);
            var cowRateList = Core.Model.Rate.GetRates(Core.MilkType.COW,Common.ConnectionString);
            dynamic model = new System.Dynamic.ExpandoObject();
            model.BuffaloRateList = Core.Model.Rate.GetRates(Core.MilkType.BUFFALO, Common.ConnectionString);//buffaloRateList;
            model.CowRateList = Core.Model.Rate.GetRates(Core.MilkType.COW, Common.ConnectionString);//cowRateList;
            return View(model);
            
        }

        // GET: FatRate/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FatRate/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FatRate/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Core.Model.Rate rate)
        {
            try
            {
                rate.Save(Common.ConnectionString);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FatRate/Edit/5
        public ActionResult Edit(int id)
        {
            Core.Model.Rate rate = new Core.Model.Rate(id, Common.ConnectionString);
            return View(rate);
        }

        // POST: FatRate/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Core.Model.Rate rate)
        {
            try
            {
                rate.Save(Common.ConnectionString);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FatRate/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Core.Model.Rate.DeleteRate(id, Common.ConnectionString);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
