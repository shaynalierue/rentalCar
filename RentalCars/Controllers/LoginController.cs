using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RentalCars.Handlers;
using System.Security.Claims;

namespace RentalCars.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginHandler _loginHandler;

        public LoginController(LoginHandler loginHandler)
        {
            _loginHandler = loginHandler;
        }

        // GET: /Login/
        //Index() -> Page Index di Folder Login
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string username, string password, string returnUrl = null)
        {
            if (_loginHandler.HandleLogin(username, password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username)
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                // Redirect ke returnUrl jika ada, atau ke Home jika tidak ada
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Username atau password salah";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Hapus autentikasi pengguna
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirect ke halaman Home atau Login setelah logout
            return RedirectToAction("Index", "Home");
        }

    }
}
