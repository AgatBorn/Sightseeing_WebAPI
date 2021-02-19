using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Features.Cities.Commands.CreateCity
{
    public class CityDto
    {
        public Guid CityId { get; set; }
        public string Name { get; set; }
        public Guid CountryId { get; set; }
    }
}
