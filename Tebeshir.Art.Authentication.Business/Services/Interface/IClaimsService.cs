using System.Security.Claims;
using Tebeshir.Art.Authentication.Domain.Models;

namespace Tebeshir.Art.Authentication.Business.Services.Interface
{
    public interface IClaimsService
    {
        Task<List<Claim>> GetUserClaimsAsync(User user);

    }
}
