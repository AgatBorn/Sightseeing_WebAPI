using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Models.Identity
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double DurationInMin { get; set; }
    }
}
