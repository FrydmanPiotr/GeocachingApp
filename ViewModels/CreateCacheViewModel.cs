using GeocachingApp.Data.Enum;
using GeocachingApp.Models;

namespace GeocachingApp.ViewModels
{
    public class CreateCacheViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public IFormFile Image { get; set; }
        public CacheType CacheType { get; set; }
    }
}
