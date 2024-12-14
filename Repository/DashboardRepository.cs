using GeocachingApp.Data;
using GeocachingApp.Interfaces;
using GeocachingApp.Models;

namespace GeocachingApp.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Club>> GetAllUserClubs()
        {
            var curUser = _httpContextAccessor.HttpContext?.User;
            var userClubs = _context.Clubs.Where(r => r.AppUser.Id.ToString() == curUser.ToString());
            return userClubs.ToList();
        }

        public async Task<List<Cache>> GetAllUserCaches()
        {
            var curUser = _httpContextAccessor.HttpContext?.User;
            var userCaches = _context.Caches.Where(r => r.AppUser.Id == curUser.ToString());
            return userCaches.ToList();
        }
    }
}
