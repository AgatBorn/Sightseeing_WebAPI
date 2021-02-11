using Sightseeing.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Domain.Entities
{
    public class AttractionCategory : BaseEntity
    {
        public Guid AttractionCategoryId { get; set; }
        public string Name { get; set; }
    }
}
