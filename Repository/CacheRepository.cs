using GeocachingApp.Data;
using GeocachingApp.Interfaces;
using GeocachingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GeocachingApp.Repository
{
    public class CacheRepository : ICacheRepository
    {
        private readonly ApplicationDbContext _context;

        public CacheRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Cache cache)
        {
            _context.Add(cache);
            return Save();
        }

        public bool Delete(Cache cache)
        {
            _context.Remove(cache);
            return Save();
        }

        public async Task<IEnumerable<Cache>> GetAll()
        {
            return await _context.Caches.ToListAsync();
        }

        public async Task<IEnumerable<Cache>> GetAllCachesByCity(string city)
        {
            return await _context.Caches.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public async Task<Cache> GetByIdAsync(int id)
        {
            return await _context.Caches.Include(i=>i.Address).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Cache> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Caches.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Cache cache)
        {
            _context.Update(cache);
            return Save();
        }
    }
}
