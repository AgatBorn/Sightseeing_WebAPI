using Sightseeing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sightseeing.Application.Contracts.Persistence
{
    public interface IAttractionRepository : IAsyncRepository<Attraction>
    {
        Task<Attraction> GetByIdWithRelatedDataAsync(Guid id);
        Task<IReadOnlyList<Attraction>> GetAllWithRelatedDataAsync();
    }
}
