using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sightseeing.Application.Contracts.Identity;
using Sightseeing.Application.Models.Identity;
using Sightseeing.Identity.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Sightseeing.Identity.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<SignUpResponse> SignUp(SignUpRequest request)
        {
            var userName = await _userManager.FindByNameAsync(request.UserName);

            if (userName != null)
            {
                throw new Exception($"Username {request.UserName} already exists.");
            }

            var email = await _userManager.FindByEmailAsync(request.Email);

            if (email != null)
            {
                throw new Exception($"Email {request.UserName} already exists.");
            }

            var newUser = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(newUser, request.Password);

            if (!result.Succeeded)
            {
                string errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += ($"{error.Code}: {error.Description} ");
                }

                throw new Exception(string.Join("\n", errors));
            }

            return new SignUpResponse()
            {
                Username = newUser.UserName
            };
        }

        public async Task<SignInResponse> SignIn(SignInRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new Exception("Invalid credentials.");
            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

            if (!result.Succeeded)
            {
                throw new Exception("Invalid credentials.");
            }

            List<Claim> userClaims = await GetUserClaims(user);

            var accessToken = _tokenService.GenerateAccessToken(user.Id, user.UserName, userClaims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await _userManager.UpdateAsync(user);

            var response = new SignInResponse
            {
                Username = user.UserName,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                RefreshToken = refreshToken
            };

            return response;
        }

        public async Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest request)
        {
            var accessToken = request.AccessToken;
            var refreshToken = request.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            var user = await _userManager.GetUserAsync(principal);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new Exception("Invalid request");
            }

            List<Claim> userClaims = await GetUserClaims(user);

            var newAccessToken = _tokenService.GenerateAccessToken(user.Id, user.UserName, userClaims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await _userManager.UpdateAsync(user);

            return new RefreshTokenResponse()
            {
                Username = user.UserName,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken
            };
        }

        public async Task RevokeToken(ClaimsPrincipal principal)
        {
            var user = await _userManager.GetUserAsync(principal);
            if (user == null)
            {
                throw new Exception("Invalid request");
            }

            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);
        }

        private async Task<List<Claim>> GetUserClaims(ApplicationUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var userClaims = claims.OfType<Claim>().ToList();

            foreach (var role in roles)
            {
                userClaims.Add(new Claim("roles", role));
            }

            return userClaims;
        }
    }
}
