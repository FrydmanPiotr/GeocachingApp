using Microsoft.AspNetCore.Mvc;

namespace GeocachingApp.Controllers
{
    public class ClubController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
