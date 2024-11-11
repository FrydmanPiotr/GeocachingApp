using GeocachingApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GeocachingApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        
        }
        public DbSet<Cache> Caches { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Address> Address { get; set; }
    }

}
