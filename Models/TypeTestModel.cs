using System.ComponentModel.DataAnnotations;

#nullable disable
namespace Typings.Models
{
    public class TypeTestModel
    {
        [Range(0, 1000)]
        public int Wpm { get; set; }
        
        [Range(0, 100)]
        public int Accuracy { get; set; }
    }
}