using System.ComponentModel.DataAnnotations;

namespace Typings.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        [Display(Prompt = "Current password...")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        
        [Required]
        [Display(Prompt = "New password...")]
        [DataType(DataType.Password)]
        [StringLength(512, MinimumLength = 6, ErrorMessage = "Password must be at least {2} characters long.")]
        public string NewPassword { get; set; }
        
        [Required]
        [Display(Prompt = "Repeat new password...")]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string RepeatPassword { get; set; }
    }
}