using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable
namespace Typings.Data
{
    public class TestResult
    {
        [Key]
        public int Id { get; set; }
        
        public int Wpm { get; set; }
        
        public int Accuracy { get; set; }
        
        [ForeignKey("Author")]
        public string AuthorUsername { get; set; }
        public User Author { get; set; }
    }
}