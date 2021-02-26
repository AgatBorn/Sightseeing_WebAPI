using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Features.Countries.Queries.GetAllCountries
{
    public class GetAllCountriesQuery : IRequest<CountriesListVm>
    {
    }
}
