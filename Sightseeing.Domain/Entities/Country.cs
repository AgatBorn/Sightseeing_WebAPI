using Sightseeing.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Domain.Entities
{
    public class Country : BaseEntity
    {
        public Guid CountryId { get; set; }
        public string Name { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}
