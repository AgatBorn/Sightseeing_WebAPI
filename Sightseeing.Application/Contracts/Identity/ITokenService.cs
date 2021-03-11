using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sightseeing.Application.Contracts.Identity
{
    public interface ITokenService
    {
        JwtSecurityToken GenerateAccessToken(string userId, string userName, List<Claim> userClaims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken);

    }
}
