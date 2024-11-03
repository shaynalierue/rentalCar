using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RentalCars.Handlers;
using RentalCars.Models;
using System.Diagnostics;

namespace RentalCars.Controllers
{
    public class HomeController : Controller
    {
        private readonly HomeHandler _homeHandler;

        public HomeController(HomeHandler homeHandler)
        {
            _homeHandler = homeHandler;
        }

        //Index() -> Page Index di Folder Home
        public IActionResult Index()
        {
            // Memanggil metode untuk memperbarui gambar mobil
            _homeHandler.UpdateCarImages(); 

            //assign hasDateSelected viewbag jadi false dulu
            ViewBag.HasDateSelected = false;

            var years = _homeHandler.GetAvailableYears();

            //memastikan years tidak null
            ViewBag.Years = years ?? new List<int?>();

            return View();
        }

        // Metode POST untuk memproses form dan redirect ke metode GET CheckDate
        //Ini biar ga bermasalah pas back page
        [HttpPost]
        public IActionResult SearchCars(DateTime pickup, DateTime returns, int? tahun, string sortOrder = "", int page = 1)
        {
            // Validasi: pastikan return date >= pickup date
            if (returns < pickup)
            {
                ViewBag.ErrorMessage = "Return Date harus lebih besar atau sama dengan Pickup Date.";
                ViewBag.Years = _homeHandler.GetAvailableYears();
                return View("Index"); // Tampilkan view dengan pesan kesalahan
            }

            // Redirect ke metode GET CheckDate dengan parameter yang sama
            return RedirectToAction("CheckDate", new { pickup = pickup, returns = returns, tahun = tahun, sortOrder = sortOrder, page = page });
        }

        // Metode GET untuk menampilkan hasil pencarian
        [HttpGet]
        public IActionResult CheckDate(DateTime pickup, DateTime returns, int? tahun, string sortOrder = "", int page = 1)
        {
            
            ViewBag.HasDateSelected = true;
            ViewBag.PickupDate = pickup;
            ViewBag.ReturnDate = returns;
            ViewBag.SelectedYear = tahun;
            ViewBag.SortOrder = sortOrder;

            // Ambil data mobil yang tersedia berdasarkan parameter pencarian
            var (availableCars, totalPages) = _homeHandler.GetAvailableCars(pickup, returns, tahun, page, 10, sortOrder);

            ViewBag.AvailableCars = availableCars;
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.Years = _homeHandler.GetAvailableYears();

            return View("Index");
        }


    }
}
