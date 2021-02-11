using Sightseeing.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Domain.Entities
{
    public class City : BaseEntity
    {
        public Guid CityId { get; set; }
        public string Name { get; set; }
        public ICollection<Attraction> Attractions { get; set; }
    }
}
