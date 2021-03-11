using Sightseeing.Application.Models.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Sightseeing.Application.Contracts.Identity
{
    public interface IAuthenticationService
    {
        Task<SignUpResponse> SignUp(SignUpRequest request);
        Task<SignInResponse> SignIn(SignInRequest request);
        Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest request);
        Task RevokeToken(ClaimsPrincipal principal);
    }
}
