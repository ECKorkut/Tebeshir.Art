using Microsoft.AspNetCore.Identity;


namespace Tebeshir.Art.Authentication.Domain.Models
{
    public class User:IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

    }
}
