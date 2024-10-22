using GeocachingApp.Models;

namespace GeocachingApp.Interfaces
{
    public interface ICacheRepository
    {
        Task<IEnumerable<Cache>> GetAll();
        Task<Cache> GetByIdAsync(int id);
        Task<Cache> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Cache>> GetAllCachesByCity(string city);
        bool Add(Cache cache);
        bool Update(Cache cache);
        bool Delete(Cache cache);
        bool Save();
    }
}
