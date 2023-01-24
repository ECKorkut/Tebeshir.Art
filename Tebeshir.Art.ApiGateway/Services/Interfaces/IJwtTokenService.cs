﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Tebeshir.Art.ApiGateway.Services.Interface
{
    public interface IJwtTokenService
    {
        JwtSecurityToken GetJwtToken(List<Claim> userClaims);

    }
}
