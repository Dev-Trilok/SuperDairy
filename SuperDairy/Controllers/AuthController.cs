using Microsoft.AspNetCore.Mvc;
using SuperDairy.Models.Auth;
using SuperDairy.Models;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
namespace SuperDairy.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login(string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        public ActionResult Forbidden()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login( LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = Core.Model.User.UserLogin(Common.ConnectionString, model.Username, model.Password);
                if (user != null)
                {
                    ClaimIdentity(user);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(nameof(LoginViewModel.Password), "Invalid Credentials! ");
                }


            }
            return View();

        }

        private void ClaimIdentity(Core.UserInfo user)
        {
            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Role, user.Role.ToString()),
                    };
            if (user.Role == Core.UserRole.ADMIN)
                claims.Add(new Claim(ClaimTypes.System, "1"));
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //var authProperties = new AuthenticationProperties { ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1), IsPersistent = true };
            HttpContext.SignInAsync(
                 CookieAuthenticationDefaults.AuthenticationScheme,
                 new ClaimsPrincipal(claimsIdentity)).Wait();
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var con = Models.Common.ConnectionString;
                if (!Core.Model.User.CheckPhoneExists(con, model.ContactNo))
                {
                    //Json mod = 
                    Core.Model.User user = new Core.Model.User
                    {
                        Name = model.Name,
                        Address = String.Empty,
                        ContactNo = model.ContactNo,
                        Password = model.Password,
                        CreatedDate = DateTime.Now,
                        LastModified = DateTime.Now,
                        IsActive = true,
                        Role = Core.UserRole.ADMIN,
                    };
                    user.Save(con);
                    return RedirectToAction("Login", "Auth");
                }
                else
                {
                    ModelState.AddModelError(nameof(RegisterViewModel.ContactNo), "Contact No Already Exists ");

                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            return RedirectToAction(nameof(Login));
        }
    }
}
