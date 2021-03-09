using Microsoft.EntityFrameworkCore;
using Sightseeing.Application.Contracts.Persistence;
using Sightseeing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sightseeing.Persistence.Repositories
{
    public class AttractionCategoryRepository : BaseRepository<AttractionCategory>, IAttractionCategoryRepository
    {
        public AttractionCategoryRepository(SightseeingDbContext dbContext) : base(dbContext)
        {
        }

        public Task<AttractionCategory> GetByIdWithRelatedDataAsync(Guid id)
        {
            var category = _dbContext.AttractionCategories.Include(c => c.Attractions).ThenInclude(c => c.City).FirstOrDefaultAsync(c => c.AttractionCategoryId == id);

            return category;
        }
    }
}
