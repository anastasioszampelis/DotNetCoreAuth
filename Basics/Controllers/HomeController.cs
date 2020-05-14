using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Basics.Controllers
{
    public class HomeController : Controller
    {
        

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Username = User.Identity.Name;
            }
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if(password == "test")
            {
                var grandmaClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim("Grandma.Says", "Very nice boi."),
                };

                var licenseClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, "Bob K Foo"),
                    new Claim("DrivingLicense", "A+"),
                };

                var grandmaIdentity = new ClaimsIdentity(grandmaClaims, "Grandma Identity");
                var licenseIdentity = new ClaimsIdentity(licenseClaims, "Government");

                var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity, licenseIdentity });

                HttpContext.SignInAsync(userPrincipal);
            }
            

            return RedirectToAction("Index");
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
