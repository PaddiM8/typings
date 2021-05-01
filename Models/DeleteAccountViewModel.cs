using System.ComponentModel.DataAnnotations;

namespace Typings.Models
{
    public class DeleteAccountViewModel
    {
        [Required]
        [Display(Prompt = "Password...")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}