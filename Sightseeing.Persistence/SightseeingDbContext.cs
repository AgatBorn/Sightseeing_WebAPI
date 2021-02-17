using Microsoft.EntityFrameworkCore;
using Sightseeing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Persistence
{
    public class SightseeingDbContext : DbContext
    {
        public SightseeingDbContext(DbContextOptions<SightseeingDbContext> options) : base(options)
        {

        }

        public DbSet<Attraction> Attractions { get; set; }
        public DbSet<AttractionCategory> AttractionCategories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }

    }
}
