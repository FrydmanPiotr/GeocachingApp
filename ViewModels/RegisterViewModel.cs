using System.ComponentModel.DataAnnotations;

namespace GeocachingApp.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name="Email address")]
        [Required(ErrorMessage="Email address is required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
            ErrorMessage = "Please enter a valid email address.")]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[a-z])(?=.*[\W_]).*$", 
            ErrorMessage = "Password must contain at least one uppercase letter, one number, and one special character.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Display(Name ="Confirm password")]
        [Required(ErrorMessage ="Confirm password is required")]
        [Compare("Password",ErrorMessage ="Password do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}