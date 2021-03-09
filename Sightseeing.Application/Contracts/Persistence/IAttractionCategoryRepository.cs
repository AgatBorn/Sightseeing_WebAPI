using Sightseeing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sightseeing.Application.Contracts.Persistence
{
    public interface IAttractionCategoryRepository : IAsyncRepository<AttractionCategory>
    {
        Task<AttractionCategory> GetByIdWithRelatedDataAsync(Guid id);
    }
}
