using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CoupletsModel
    {
        [Required(ErrorMessage = "Line 1 is required.")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "Only letters and spaces are allowed.")]
        public string? Line1 { get; set; }

        [Required(ErrorMessage = "Line 2 is required.")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "Only letters and spaces are allowed.")]
        public string? Line2 { get; set; }
    }

}
