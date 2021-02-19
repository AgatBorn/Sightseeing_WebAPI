using Sightseeing.Application.Contracts.Persistence;
using Sightseeing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sightseeing.Persistence.Repositories
{
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        public CountryRepository(SightseeingDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> IsCountryNameUnique(string name)
        {
            var hasCountryWithThatName = _dbContext.Countries.Any(x => x.Name == name);

            return Task.FromResult(!hasCountryWithThatName);
        }
    }
}
