using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeocachingApp.Models
{

    public class AppUser : IdentityUser
    {
        [Key]
        public int Id { get; set; }
        public int? CachesFound { get; set; }
        public int? CachesCreated { get; set; }

        [ForeignKey("Address")]
        public int? AddressId {  get; set; } 
        public Address? Address { get; set; }
        public ICollection<Club> Clubs { get; set; }
        public ICollection<Cache> Caches { get; set; }
    }
}