using GeocachingApp.Data;
using GeocachingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public IActionResult Detail(int id)
        {
            Cache cache = _context.Caches.Include(a => a.Address).FirstOrDefault(c => c.Id == id);
            return View(cache);
        }
    }
}
