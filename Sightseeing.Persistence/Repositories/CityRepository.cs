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

        public Task<bool> IsCityNameUnique(string name)
        {
            var hasCityWithThatName = _dbContext.Cities.Any(x => x.Name == name);

            return Task.FromResult(!hasCityWithThatName);
        }
    }
}
