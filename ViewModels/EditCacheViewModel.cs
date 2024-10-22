using GeocachingApp.Data.Enum;
using GeocachingApp.Models;

namespace GeocachingApp.ViewModels
{
    public class EditCacheViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? URL { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public IFormFile Image { get; set; }
        public CacheType CacheType { get; set; }
    }
}
