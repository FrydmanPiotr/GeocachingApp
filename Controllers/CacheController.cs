using GeocachingApp.Data;
using GeocachingApp.Interfaces;
using GeocachingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeocachingApp.Controllers
{
    public class CacheController : Controller
    {
        private readonly ICacheRepository _cacheRepository;

        public CacheController(ICacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }
        
        public async Task<IActionResult> Index()
        {
            IEnumerable<Cache> caches = await _cacheRepository.GetAll();
            return View(caches);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Cache cache = await _cacheRepository.GetByIdAsync(id);
            return View(cache);
        }
    }
}
