using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sightseeing.Api.IntegrationTests.Common
{
    public static class ApiTokenHelper
    {
        private static readonly string _secretKey = "SightseeingSuperS@cretKey";
        private static readonly string _issuer = "SightseeingIdentity";
        private static readonly string _audience = "SightseeingIdentityUser";
        private static readonly int _durationInMin = 30;

        private static readonly string _fakeUserId = "1";
        private static readonly string _fakeUserName = "Agata";

        public static string GenerateFakeToken()
        {
            var userClaims = new List<Claim>();
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, _fakeUserId));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, _fakeUserName));

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var accessToken = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(_durationInMin),
                signingCredentials: signingCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(accessToken);

            return token;
        }

    }
}
