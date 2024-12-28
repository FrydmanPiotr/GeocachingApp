using GeocachingApp.Interfaces;
using GeocachingApp.Models;
using GeocachingApp.Repository;
using GeocachingApp.Services;
using GeocachingApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GeocachingApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IPhotoService _photoService;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers();
            List<UserViewModel> result = new List<UserViewModel>();
            foreach(var user in users)
            {
                var userViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    CachesFound = user.CachesFound,
                    CachesCreated=user.CachesCreated,
                    ProfileImageUrl = user.ProfileImageUrl,
                };
                result.Add(userViewModel);
            }
            return View(result);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetUserById(id);
            var userDetailViewModel = new UserDetailViewModel()
            {
                Id=user.Id,
                UserName = user.UserName,
                CachesFound = user.CachesFound,
                CachesCreated=user.CachesCreated,
                City =user.City,
                Country=user.Country,
            };
            return View(userDetailViewModel);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null) return View("Error");
            var userVM = new EditUserProfileViewModel
            {
                CachesFound = user.CachesFound,
                CachesCreated = user.CachesCreated,
                ProfileImageUrl = user.ProfileImageUrl,
                City = user.City,
                Country = user.Country,
    };
            return View(userVM);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, EditUserProfileViewModel userVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit user");
                return View("Edit", userVM);
            }

            var userDetails = await _userRepository.GetByIdAsyncNoTracking(id);

            if (userDetails != null)
            {
                var user = new AppUser
                {
                    Id = id,
                    CachesFound = userVM.CachesFound,
                    CachesCreated = userVM.CachesCreated,
                    ProfileImageUrl = userVM.ProfileImageUrl,
                    City = userVM.City,
                    Country = userVM.Country,
                };

                _userRepository.Update(user);

                return RedirectToAction("Index");
            }
            else
            {
                return View(userVM);
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            
            var userDetails = await _userRepository.GetUserById(id);
            if (userDetails == null) return View("Error");
            if (User.Identity.Name == userDetails.UserName && User.IsInRole("admin"))
            {
                ModelState.AddModelError("", "Administrator nie może usunąć własnego konta.");
                return RedirectToAction("Index");
            }
            return View(userDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var userDetails = await _userRepository.GetUserById(id);
            if (userDetails == null) return View("Error");
            
            _userRepository.Delete(userDetails);
            return RedirectToAction("Index");
        }
    }
}
