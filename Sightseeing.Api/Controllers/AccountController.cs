using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sightseeing.Application.Contracts.Identity;
using Sightseeing.Application.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sightseeing.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("sign-up")]
        public async Task<ActionResult<SignInResponse>> SignUp([FromBody] SignUpRequest request)
        {
            var result = await _authenticationService.SignUp(request);

            return Ok(result);
        }

        [HttpPost("sign-in")]
        public async Task<ActionResult<SignInResponse>> SignIn([FromBody] SignInRequest request)
        {
            var result = await _authenticationService.SignIn(request);

            return Ok(result);
        }

        [HttpPost("refreshToken")]
        public async Task<ActionResult<RefreshTokenResponse>> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var result = await _authenticationService.RefreshToken(request);

            return Ok(result);
        }

        [Authorize]
        [HttpPost("revokeToken")]
        public async Task<ActionResult<RefreshTokenResponse>> Revoke()
        {
            await _authenticationService.RevokeToken(User);

            return NoContent();
        }
    }
}
