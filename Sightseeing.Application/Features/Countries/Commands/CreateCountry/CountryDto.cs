using System;

namespace Sightseeing.Application.Features.Countries.Commands.CreateCountry
{
    public class CountryDto
    {
        public Guid CountryId { get; set; }
        public string Name { get; set; }
    }
}