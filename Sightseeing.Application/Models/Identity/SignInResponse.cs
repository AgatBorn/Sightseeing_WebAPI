using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Sightseeing.Application.Models.Identity
{
    public class SignInResponse
    {
        public string Username { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
