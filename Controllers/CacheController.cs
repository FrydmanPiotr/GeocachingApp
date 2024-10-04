using GeocachingApp.Data;
using GeocachingApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeocachingApp.Controllers
{
    public class CacheController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CacheController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Cache> caches = _context.Caches.ToList();
            return View(caches);
        }
    }
}
