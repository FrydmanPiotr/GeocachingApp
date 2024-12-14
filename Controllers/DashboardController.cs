using GeocachingApp.Data;
using GeocachingApp.Interfaces;
using GeocachingApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GeocachingApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
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
    }
}
