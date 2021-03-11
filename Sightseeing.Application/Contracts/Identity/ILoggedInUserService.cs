using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Contracts.Identity
{
    public interface ILoggedInUserService
    {
        string UserId { get; }
    }
}
