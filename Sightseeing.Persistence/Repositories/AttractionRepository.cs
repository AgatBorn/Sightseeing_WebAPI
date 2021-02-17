using Microsoft.EntityFrameworkCore;
using Sightseeing.Application.Contracts.Persistence;
using Sightseeing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sightseeing.Persistence.Repositories
{
    public class AttractionRepository : BaseRepository<Attraction>, IAttractionRepository
    {
        public AttractionRepository(SightseeingDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Attraction> GetByIdWithRelatedDataAsync(Guid id)
        {
            return await _dbContext.Attractions.Include(x => x.Category).Include(x => x.City).FirstOrDefaultAsync(x => x.AttractionId == id);
        }
    }
}
