using Sightseeing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sightseeing.Application.Contracts.Persistence
{
    public interface ICountryRepository : IAsyncRepository<Country>
    {
        Task<bool> IsCountryNameUnique(string name);
    }
}
