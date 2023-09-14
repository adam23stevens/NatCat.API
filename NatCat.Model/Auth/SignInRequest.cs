using System.ComponentModel.DataAnnotations;

namespace NatCat.Model.Auth
{
    public class SignInRequestDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^[a-zA-Z0-9_.-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}

