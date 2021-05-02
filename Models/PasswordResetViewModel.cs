using System.ComponentModel.DataAnnotations;

#nullable disable
namespace Typings.Models
{
    public class PasswordResetViewModel
    {
        [Required]
        [Display(Name = "New password", Prompt = "New password...")]
        [DataType(DataType.Password)]
        [StringLength(512, MinimumLength = 6, ErrorMessage = "Password must be at least {2} characters long.")]
        public string NewPassword { get; set; }
        
        [Required]
        [Display(Name = "Repeat password", Prompt = "Repeat new password...")]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string RepeatPassword { get; set; }
        
        public string UserId { get; set; }
        
        public string Code { get; set; }
    }
}