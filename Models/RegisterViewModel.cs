using System.ComponentModel.DataAnnotations;

#nullable disable
namespace Typings.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Prompt = "Username...")]
        [RegularExpression("[A-z0-9_-]+", ErrorMessage = "Username may only contain English letters, digits, underscores, and dashes.")]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "Username length must be between {2} and {1}.")]
        public string Username { get; set; }
        
        [Required]
        [Display(Prompt = "Password...")]
        [DataType(DataType.Password)]
        [StringLength(512, MinimumLength = 6, ErrorMessage = "Password must be at least {2} characters long.")]
        public string Password { get; set; }
        
        [Required]
        [Display(Name = "Password again", Prompt = "Repeat password...")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords don't match.")]
        public string RepeatPassword { get; set; }
    }
}