using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAuth.Models
{
    public class RegisterModel
    {
        [StringLength(320, ErrorMessage = "EmailLength")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
            ErrorMessage = "EmailRegularExpression")]
        [Required(ErrorMessage = "EmailRequired")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(255, MinimumLength = 4, ErrorMessage = "PasswordLength")]
        [Required(ErrorMessage = "PasswordRequired")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "PasswordConfirmRequired")]
        [DataType(DataType.Password)]
        [Display(Name = "PasswordConfirm")]
        [Compare("Password", ErrorMessage = "PasswordConfirmCompare")]
        public string PasswordConfirm { get; set; }
    }
}
