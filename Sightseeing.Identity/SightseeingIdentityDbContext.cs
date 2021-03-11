using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sightseeing.Identity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Identity
{
    public class SightseeingIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public SightseeingIdentityDbContext(DbContextOptions<SightseeingIdentityDbContext> options) : base(options)
        {
        }
    }
}
