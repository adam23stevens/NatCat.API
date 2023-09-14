using System;
namespace NatCat.Model.Auth
{
    public class SignInResponseDTO
    {
        public bool IsAuthSuccessful { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Token { get; set; }
        public UserDTO? userDTO { get; set; }
    }
}

