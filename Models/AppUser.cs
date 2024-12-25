using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeocachingApp.Models
{

    public class AppUser : IdentityUser
    {
        public int? CachesFound { get; set; }
        public int? CachesCreated { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        [ForeignKey("Address")]
        public int? AddressId { get; set; } 
        public Address? Address { get; set; }
        public ICollection<Club> Clubs { get; set; }
        public ICollection<Cache> Caches { get; set; }
    }
}