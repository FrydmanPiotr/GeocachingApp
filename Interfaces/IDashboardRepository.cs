using GeocachingApp.Models;

namespace GeocachingApp.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Club>> GetAllUserClubs();
        Task<List<Cache>> GetAllUserCaches();
    }
}
