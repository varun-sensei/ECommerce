using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Models;
using System.Security.Claims;

namespace OnlineShopping.Controllers
{
    public class SecurityController : Controller
    {
        onlineshopdbContext dc = new onlineshopdbContext();
        public IActionResult Home()
        {
            return View();
        }

        // Very secured and sensitive method.
        [Authorize(Roles ="Pirate King")]
        public IActionResult SecuredMethod()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string txtuser, string txtpass)
        {
            var res = (from t in dc.Registers
                       where t.Uname == txtuser && t.Password == txtpass
                       select t).FirstOrDefault();
            if (res != null)
            {
                var userClaims = new List<Claim>()
             {
             new Claim(ClaimTypes.Name, res.Uname),
             new Claim(ClaimTypes.Role,res.Designation),
              new Claim(ClaimTypes.Country, res.Country),
             };

                var identity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("SecuredMethod", "Security");
            }
            else
            {

                ViewData["msg"] = "Invalid username or password";
            }

            return View();
        }

        public IActionResult Logout()
        {
            var res = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }


        public IActionResult AccessDeniedPage()
        {
            return View();
        }
    }
}
