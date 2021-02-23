using Sightseeing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Contracts.Persistence
{
    public interface IAttractionCategoryRepository : IAsyncRepository<AttractionCategory>
    {
    }
}
