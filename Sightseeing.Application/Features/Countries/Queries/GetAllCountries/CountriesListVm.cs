using System.Collections.Generic;

namespace Sightseeing.Application.Features.Countries.Queries.GetAllCountries
{
    public class CountriesListVm
    {
        public List<CountryDto> Countries { get; set; }
        public int Count { get; set; }
    }
}