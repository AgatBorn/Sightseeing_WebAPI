using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Features.Cities.Queries.GetAllCities
{
    public class CitiesListVm
    {
        public List<CityDto> Cities { get; set; }
        public int Count { get; set; }
    }
}
