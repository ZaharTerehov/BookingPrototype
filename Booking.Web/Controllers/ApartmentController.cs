using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Controllers
{
    public class ApartmentController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return  View();
        }
    }
}
