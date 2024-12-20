using GeocachingApp.Interfaces;
using GeocachingApp.Models;
using GeocachingApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GeocachingApp.Controllers
{
    public class CacheController : Controller
    {
        private readonly ICacheRepository _cacheRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CacheController(ICacheRepository cacheRepository, 
            IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _cacheRepository = cacheRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
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

        public IActionResult Create()
        {
            var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var createCacheViewModel = new CreateCacheViewModel { AppUserId=curUserId };
            return View(createCacheViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCacheViewModel cacheVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(cacheVM.Image);

                var cache = new Cache
                {
                    Title = cacheVM.Title,
                    Description = cacheVM.Description,
                    Image = result.Url.ToString(),
                    AppUserId = cacheVM.AppUserId,
                    Address = new Address
                    {
                        Street = cacheVM.Address.Street,
                        City = cacheVM.Address.City,
                        Country = cacheVM.Address.Country
                    }
                };
                _cacheRepository.Add(cache);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(cacheVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var cache = await _cacheRepository.GetByIdAsync(id);
            if (cache == null) return View("Error");
            var cacheVM = new EditCacheViewModel
            {
                Title = cache.Title,
                Description = cache.Description,
                AddressId = cache.AddressId,
                Address = cache.Address,
                URL = cache.Image
            };
            return View(cacheVM);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditCacheViewModel cacheVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit cache");
                return View("Edit", cacheVM);
            }

            var userCache = await _cacheRepository.GetByIdAsyncNoTracking(id);

            if (userCache != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userCache.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(cacheVM);
                }

                var photoResult = await _photoService.AddPhotoAsync(cacheVM.Image);

                var cache = new Cache
                {
                    Id = id,
                    Title = cacheVM.Title,
                    Description = cacheVM.Description,
                    Image = photoResult.Url.ToString(),
                    AddressId = cacheVM.AddressId,
                    Address = cacheVM.Address
                };

                _cacheRepository.Update(cache);

                return RedirectToAction("Index");
            }
            else
            {
                return View(cacheVM);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var cacheDetails = await _cacheRepository.GetByIdAsync(id);
            if (cacheDetails == null) return View("Error");
            return View(cacheDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var cacheDetails = await _cacheRepository.GetByIdAsync(id);
            if (cacheDetails == null) return View("Error");

            _cacheRepository.Delete(cacheDetails);
            return RedirectToAction("Index");
        }
    }
}
