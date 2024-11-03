using Microsoft.AspNetCore.Mvc;
using RentalCars.Handlers;
using RentalCars.Models;

namespace RentalCars.Controllers
{
    public class RegistrasiController : Controller
    {
        private readonly RegistrasiHandler _registrasiHandler;

        public RegistrasiController(RegistrasiHandler registrasiHandler)
        {
            _registrasiHandler = registrasiHandler;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(MsCustomer newUser)
        {
            if (_registrasiHandler.isEmailRegistered(newUser.email))
            {
                ViewBag.ErrorMessage = "Email sudah terdaftar";
                return View("Index");
            }

            //ADD user
            if (_registrasiHandler.RegisterUser(newUser))
            {
                ViewBag.ErrorMessage = "Akun berhasil terdaftar";
                return RedirectToAction("Index","Login");
            }

            //Jika tidak berhasil Registrasi
            ViewBag.ErrorMessage = "Registrasi gagal. Silahkan coba lagi";
            return View("Index");
        }

    }
}
