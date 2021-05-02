using System.ComponentModel.DataAnnotations;

#nullable disable
namespace Typings.Models
{
    public class EmailViewModel
    {
        [Required]
        [Display(Prompt = "Email address...")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}