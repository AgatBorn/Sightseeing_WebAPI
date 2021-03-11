using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Models.Identity
{
    public class SignInRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
