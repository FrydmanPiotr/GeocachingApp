﻿using GeocachingApp.Models;

namespace GeocachingApp.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Club>> GetAllUserClubs();
        Task<List<Cache>> GetAllUserCaches();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetByIdNoTracking(string id);
        bool Update(AppUser user);
        bool Save();
    }
}
