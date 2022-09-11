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

        public UsersController()
        {
        }
        // GET: UsersController
        public ActionResult Index()
        {
            var list = Core.Model.User.GetUsers(Common.ConnectionString);
            return View(model: list);
        }

        // GET: UsersController/Details/5
        public ActionResult Details(int id)
        {
            Core.Model.User user = new Core.Model.User(id, Common.ConnectionString);
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
                user.LastModified = DateTime.Now;
                user.Save(Common.ConnectionString);
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
            User user = new User(id, Common.ConnectionString);
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
                user.Save(Common.ConnectionString);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(user);
            }
        }

        // Get: UsersController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Core.Model.User.DeleteUser(id, Common.ConnectionString);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
        ///* [HttpPost]
        // public IActionResult GetUser()
        // {
        //     try
        //     {
        //         var draw = Request.Form["draw"].FirstOrDefault(); // get total page size
        //         var start = Request.Form["start"].FirstOrDefault(); // get starte length size from request.
        //         var length = Request.Form["length"].FirstOrDefault();
        //         var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
        //         var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        //         var searchValue = Request.Form["search[value]"].FirstOrDefault(); // check if there is any search characters passed
        //         int pageSize = length != null ? Convert.ToInt32(length) : 0;
        //         int skip = start != null ? Convert.ToInt32(start) : 0;
        //         int recordsTotal = 0;
        //         var personData = (from tempUser in this.Context.User select tempUser); // get data from database
        //         //check for sorting column number and direction
        //         if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        //         {
        //             //get sorting column
        //             Func<User, string> orderingFunction = c => sortColumn == "ID" ? c.Id.ToString() : sortColumn == "Name" ? c.Name : c.Id.ToString();

        //             //check sort order 
        //             if (sortColumnDirection == "desc")
        //             {
        //                 personData = personData.OrderByDescending(orderingFunction).AsQueryable();
        //             }
        //             else
        //             {
        //                 personData = personData.OrderBy(orderingFunction).AsQueryable();
        //             }

        //         }

        //         // if there is any search value, filter results
        //         if (!string.IsNullOrEmpty(searchValue))
        //         {
        //             personData = personData.Where(m => m.Name.ToLower().Contains(searchValue.ToLower()));
        //         }//|| m.LastName.ToLower().Contains(searchValue.ToLower())
        //         // get total records acount
        //         recordsTotal = personData.Count();
        //         //get page data
        //         var data = personData.Skip(skip).Take(pageSize).ToList();
        //         var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
        //         return Ok(jsonData);
        //     }
        //     catch (Exception ex)
        //     {
        //         throw ex;
        //     }
        // }*/
    }
}


