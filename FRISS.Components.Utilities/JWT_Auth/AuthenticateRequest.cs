using System.ComponentModel.DataAnnotations;

namespace FRISS.Components.Utilities.JWT_Auth
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

}
