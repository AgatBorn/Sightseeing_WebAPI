using System;
using System.Collections.Generic;

namespace Sightseeing.Application.Features.Cities.Queries.GetCityDetail
{
    public class CityDetailVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CountryId { get; set; }
        public string CountryName { get; set; }
        public ICollection<CityDetailAttractionVm> Attractions { get; set; }
    }
}