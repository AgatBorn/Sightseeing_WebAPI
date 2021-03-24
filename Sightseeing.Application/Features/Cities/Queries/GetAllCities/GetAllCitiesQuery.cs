using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Features.Cities.Queries.GetAllCities
{
    public class GetAllCitiesQuery : IRequest<IList<CityListVm>>
    {
    }
}
