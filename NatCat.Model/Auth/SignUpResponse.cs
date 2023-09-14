using System;
namespace NatCat.Model.Auth
{
    public class SignUpResponseDTO
    {
        public bool IsRegistrationSuccessful { get; set; }
        public IEnumerable<string>? ErrorMessages { get; set; }
    }
}

