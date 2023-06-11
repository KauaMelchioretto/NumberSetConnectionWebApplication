using Foolproof;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NumberSetConnectionWebApplication.Models
{
    public class Functions
    {
        [Required]
        [Display(Name = "First Number"), Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int FirstNumber { get; set; }
        [Display(Name = "Second Number"), Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        [Required]
        public int SecondNumber { get; set; }
        public List<Element> Elements { get; set; }
        public string FunctionName { get; set;  }
    }
}