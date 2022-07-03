using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Model;
using SuperDairy.Models;
using Microsoft.AspNetCore.Authorization;
namespace SuperDairy.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class UsersController : Controller
    {
        // GET: UsersController
        public ActionResult Index()
        {
            var list=Core.Model.User.GetUsers(Common.GetConnectionString());
            return View(model:list);
        }

        // GET: UsersController/Details/5
        public ActionResult Details(int id)
        {
            Core.Model.User user = new Core.Model.User(id, Common.GetConnectionString());
            return View(user);
        }

        // GET: UsersController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Core.Model.User user)
        {
            try
            {
                user.CreatedDate = DateTime.Now;
                user.LastModified= DateTime.Now;
                user.Save(Common.GetConnectionString());
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(user);
            }
        }

        // GET: UsersController/Edit/5
        public ActionResult Edit(int id)
        {
            User user = new User(id, Common.GetConnectionString());
            return View(user);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            try
            {
                //user.Id = id;
                user.LastModified = DateTime.Now;
                user.Save(Common.GetConnectionString());
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(user);
            }
        }

        // Get: UsersController/Delete/5
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Core.Model.User.DeleteUser(id,Common.GetConnectionString());
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
