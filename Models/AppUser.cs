using System.ComponentModel.DataAnnotations;

namespace GeocachingApp.Models
{

    public class AppUser
    {
        [Key]
        public string Id { get; set; }
        public int? CachesFound { get; set; }
        public int? CachesCreated { get; set; }
        public Address? Address { get; set; }
        public ICollection<Club> Clubs { get; set; }
        public ICollection<Cache> Caches { get; set; }
    }
}