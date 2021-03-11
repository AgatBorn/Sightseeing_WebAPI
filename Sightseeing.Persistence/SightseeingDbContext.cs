using Microsoft.EntityFrameworkCore;
using Sightseeing.Application.Contracts.Identity;
using Sightseeing.Domain.Common;
using Sightseeing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sightseeing.Persistence
{
    public class SightseeingDbContext : DbContext
    {
        private readonly ILoggedInUserService _loggedInUserService;

        public SightseeingDbContext(DbContextOptions<SightseeingDbContext> options) : base(options)
        {

        }

        public SightseeingDbContext(DbContextOptions<SightseeingDbContext> options, ILoggedInUserService loggedInUserService) : base(options)
        {
            _loggedInUserService = loggedInUserService;
        }

        public DbSet<Attraction> Attractions { get; set; }
        public DbSet<AttractionCategory> AttractionCategories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.Now;
                        entry.Entity.CreatedBy = _loggedInUserService.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedAt = DateTime.Now;
                        entry.Entity.LastModifiedBy = _loggedInUserService.UserId;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
