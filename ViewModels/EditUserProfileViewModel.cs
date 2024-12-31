namespace GeocachingApp.ViewModels
{
    public class EditUserProfileViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public int? CachesFound { get; set; }
        public int? CachesCreated { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public IFormFile Image { get; set; }
    }
}
