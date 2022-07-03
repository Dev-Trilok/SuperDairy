using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuperDairy.Models.Auth
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Contact No")]
        [RegularExpression(@"^(\+\d{1,3}[- ]?)?\d{10}$", ErrorMessage = "Invalid Contact No.")]
        public string Username { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "{0} too short", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
