using Microsoft.AspNetCore.Mvc;

namespace GeocachingApp.Controllers
{
    public class CacheController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
