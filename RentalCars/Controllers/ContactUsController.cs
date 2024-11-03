using Microsoft.AspNetCore.Mvc;

namespace RentalCars.Controllers
{
    public class ContactUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
