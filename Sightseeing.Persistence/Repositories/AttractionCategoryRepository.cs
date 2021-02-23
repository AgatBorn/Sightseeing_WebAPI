using Sightseeing.Application.Contracts.Persistence;
using Sightseeing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Persistence.Repositories
{
    public class AttractionCategoryRepository : BaseRepository<AttractionCategory>, IAttractionCategoryRepository
    {
        public AttractionCategoryRepository(SightseeingDbContext dbContext) : base(dbContext)
        {
        }
    }
}
