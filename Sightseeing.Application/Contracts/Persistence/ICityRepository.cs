﻿using Sightseeing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sightseeing.Application.Contracts.Persistence
{
    public interface ICityRepository : IAsyncRepository<City>
    {
        Task<bool> IsCityNameUnique(string name);
        Task<City> GetByIdWithRelatedDataAsync(Guid id);
    }
}
