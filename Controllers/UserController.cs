using GeocachingApp.Interfaces;
using GeocachingApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GeocachingApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

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
