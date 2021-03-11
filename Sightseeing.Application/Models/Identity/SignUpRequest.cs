using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Models.Identity
{
    public class SignUpRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
