using CloudinaryDotNet.Actions;
using GeocachingApp.Interfaces;
using GeocachingApp.Models;
using GeocachingApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GeocachingApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _photoService;

        public DashboardController(IDashboardRepository dashboardRepository, 
            IHttpContextAccessor httpContextAccessor, IPhotoService photoService)
        {
            _dashboardRepository = dashboardRepository;
            _httpContextAccessor = httpContextAccessor;
            _photoService = photoService;
        }

        private void MapUserEdit(AppUser user, EditUserProfileViewModel editVM, ImageUploadResult photoResult)
        {
            user.Id = editVM.Id;
            user.CachesFound = editVM.CachesFound;
            user.CachesCreated = editVM.CachesCreated;
            user.ProfileImageUrl = photoResult.Url.ToString();
            user.City = editVM.City;
            user.Country = editVM.Country;
        }

        public async Task<IActionResult> Index()
        {
            var userCaches = await _dashboardRepository.GetAllUserCaches();
            var userClubs = await _dashboardRepository.GetAllUserClubs();
            var dashboardViewModel = new DashboardViewModel()
            {
                Caches = userCaches,
                Clubs = userClubs,
            };
            return View(dashboardViewModel);
        }

        public async Task<IActionResult> EditUserProfile()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _dashboardRepository.GetUserById(curUserId);
            if (user == null) return View("Error");
            var editUserViewModel = new EditUserProfileViewModel()
            {
                Id = curUserId,
                CachesFound = user.CachesFound,
                CachesCreated = user.CachesCreated,
                ProfileImageUrl = user.ProfileImageUrl,
                City = user.City,
                Country = user.Country
            };
            return View(editUserViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserProfileViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditUserProfile",editVM);
            }

            AppUser user = await _dashboardRepository.GetByIdNoTracking(editVM.Id);

            if (user.ProfileImageUrl == "" || user.ProfileImageUrl == null)
            {
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);

                MapUserEdit(user, editVM, photoResult);

                _dashboardRepository.Update(user);
                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(editVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);
                MapUserEdit(user, editVM, photoResult);
                _dashboardRepository.Update(user);
                return RedirectToAction("Index");
            }
        }
    }
}
