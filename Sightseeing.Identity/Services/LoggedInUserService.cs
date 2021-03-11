using Microsoft.AspNetCore.Http;
using Sightseeing.Application.Contracts.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Sightseeing.Identity.Services
{
    public class LoggedInUserService : ILoggedInUserService
    {
        public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string UserId { get; }
    }
}
