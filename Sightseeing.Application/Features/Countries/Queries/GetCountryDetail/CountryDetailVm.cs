using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Features.Countries.Queries.GetCountryDetail
{
    public class CountryDetailVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<CountryDetailCityVm> Cities { get; set; }
    }
}
