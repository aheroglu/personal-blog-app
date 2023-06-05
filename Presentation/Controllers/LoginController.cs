using DataAccess.Concrete;
using Entity.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        Context db = new Context();

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(User user)
        {
            var values = db.Users.FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);

            if (values != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                };

                var userIdendity = new ClaimsIdentity(claims, "a");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdendity);
                await HttpContext.SignInAsync(principal);

                HttpContext.Session.SetString("UserName", user.UserName);
                HttpContext.Session.SetString("Password", user.Password);

                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }

            else
            {
                ViewBag.ErrorMessage = "Hatalı kullanıcı adı veya parola! Tekrar deneyiniz.";
                return View(user);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("SignIn", "Login");
        }

    }
}
