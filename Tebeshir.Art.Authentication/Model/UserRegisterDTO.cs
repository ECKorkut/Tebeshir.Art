using System.ComponentModel.DataAnnotations;

namespace Tebeshir.Art.Authentication.Model
{
    public class UserRegisterDTO
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
