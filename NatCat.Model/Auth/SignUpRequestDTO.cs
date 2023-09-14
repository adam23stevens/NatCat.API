using System;
using System.ComponentModel.DataAnnotations;

namespace NatCat.Model.Auth
{
    public class SignUpRequestDTO
    {
        [Required(ErrorMessage = "Profile name is required")]
        public string? ProfileName { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [RegularExpression("^[a-zA-Z0-9_.-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords must match")]
        public string? ConfirmPassword { get; set; }

        public bool AdminOverride { get; set; }
    }
}

