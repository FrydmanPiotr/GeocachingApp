using CloudinaryDotNet.Actions;
using GeocachingApp.Interfaces;
using GeocachingApp.Models;
using GeocachingApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GeocachingApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _photoService;

        public UserController(IUserRepository userRepository,
             IHttpContextAccessor httpContextAccessor, IPhotoService photoService)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _photoService = photoService;
        }

        private void MapUserEdit(AppUser user, EditUserProfileViewModel userVM, ImageUploadResult photoResult)
        {
            user.Id = userVM.Id;
            user.CachesFound = userVM.CachesFound;
            user.CachesCreated = userVM.CachesCreated;
            user.ProfileImageUrl = photoResult.Url.ToString();
            user.City = userVM.City;
            user.Country = userVM.Country;
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

        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null) return View("Error");
            var editUserViewModel = new EditUserProfileViewModel()
            {   
                Id = user.Id,
                CachesFound = user.CachesFound,
                CachesCreated = user.CachesCreated,
                ProfileImageUrl = user.ProfileImageUrl,
                City = user.City,
                Country = user.Country
            };
            return View(editUserViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(string id, EditUserProfileViewModel userVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditUserProfile", userVM);
            }

            AppUser user = await _userRepository.GetByIdNoTracking(id);

            if (user.ProfileImageUrl == "" || user.ProfileImageUrl == null)
            {
                var photoResult = await _photoService.AddPhotoAsync(userVM.Image);

                MapUserEdit(user, userVM, photoResult);

                _userRepository.Update(user);
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
                    ModelState.AddModelError("", "Could not delete photo" + ex.Message);
                    return View(userVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(userVM.Image);
                MapUserEdit(user, userVM, photoResult);
                _userRepository.Update(user);
                return RedirectToAction("Index");
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
