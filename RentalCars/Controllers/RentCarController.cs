using Microsoft.AspNetCore.Mvc;
using RentalCars.Handlers;
using System.Threading.Tasks;
using RentalCars.Models;

namespace RentalCars.Controllers
{
    public class RentCarController : Controller
    {
        private readonly RentCarHandler _rentCarHandler;

        public RentCarController(RentCarHandler rentCarHandler)
        {
            _rentCarHandler = rentCarHandler;
        }

        public async Task<IActionResult> Index(string carId, DateTime pickup, DateTime returns)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Login", new { returnUrl = Url.Action("Index", "RentCar", new { carId }) });
            }

            var car = await _rentCarHandler.GetCarWithImageByIdAsync(carId,pickup, returns);
            if (car == null)
            {
                return NotFound("Mobil tidak ditemukan.");
            }

            // Mengirim data mobil dan tanggal sewa ke view
            ViewBag.SelectedCar = car;
            ViewBag.PickupDate = pickup;
            ViewBag.ReturnDate = returns;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRental(string carId, DateTime pickup, DateTime returns, decimal totalPrice)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Login", new { returnUrl = Url.Action("Index", "RentCar", new { carId, pickup, returns }) });
            }

            var errorMessage = await _rentCarHandler.CreateRentalAsync(carId, User.Identity.Name, pickup, returns, totalPrice);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                // Ambil data mobil dan isi ViewBag jika terjadi error
                var car = await _rentCarHandler.GetCarWithImageByIdAsync(carId, pickup, returns);
                ViewBag.SelectedCar = car;
                ViewBag.PickupDate = pickup;
                ViewBag.ReturnDate = returns;
                ViewBag.ErrorMessage = errorMessage;

                return View("Index");
            }

            return RedirectToAction("Index", "HistoryRent");
        }




    }
}
