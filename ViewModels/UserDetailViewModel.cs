namespace GeocachingApp.ViewModels
{
    public class UserDetailViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public int? CachesFound { get; set; }
        public int? CachesCreated { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
    }
}
