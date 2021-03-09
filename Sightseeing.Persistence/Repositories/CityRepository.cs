using Microsoft.EntityFrameworkCore;
using Sightseeing.Application.Contracts.Persistence;
using Sightseeing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sightseeing.Persistence.Repositories
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        public CityRepository(SightseeingDbContext dbContext) : base(dbContext)
        {
        }

        public Task<City> GetByIdWithRelatedDataAsync(Guid id)
        {
            var city = _dbContext.Cities.Include(c => c.Country).Include(c => c.Attractions).FirstOrDefaultAsync(c => c.CityId == id);

            return city;
        }

        public Task<bool> IsCityNameUnique(string name)
        {
            var hasCityWithThatName = _dbContext.Cities.Any(x => x.Name == name);

            return Task.FromResult(!hasCityWithThatName);
        }
    }
}
