using System.ComponentModel.DataAnnotations;

#nullable disable
namespace Typings.Models
{
    public class LoginViewModel
    {
        [Display(Prompt = "Username...")]
        public string Username { get; set; }
        
        [Display(Prompt = "Password...")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}