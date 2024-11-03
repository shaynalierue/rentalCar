using Microsoft.AspNetCore.Mvc;
using RentalCars.Handlers;

namespace RentalCars.Controllers
{
    public class HistoryRentController : Controller
    {
        private readonly HistoryRentHandler _historyRentHandler;

        public HistoryRentController(HistoryRentHandler historyRentHandler)
        {
            _historyRentHandler = historyRentHandler;
        }

        // Tampilkan riwayat penyewaan
        [HttpGet]
        public IActionResult Index()
        {
            var rentalHistory = _historyRentHandler.GetRentalHistory(User.Identity.Name);
            ViewBag.RentalHistory = rentalHistory;
            return View("Index");
        }
    }
}
