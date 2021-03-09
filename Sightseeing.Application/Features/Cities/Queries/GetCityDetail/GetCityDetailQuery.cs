using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Features.Cities.Queries.GetCityDetail
{
    public class GetCityDetailQuery : IRequest<CityDetailVm>
    {
        public Guid Id { get; set; }
    }
}
