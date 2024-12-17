namespace GeocachingApp.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public int? CachesFound { get; set; }
        public int? CachesCreated { get; set; }
        public string ProfileImageUrl { get; set; }
    }
}
