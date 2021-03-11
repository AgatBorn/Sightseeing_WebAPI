using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Features.Countries.Queries.GetCountryDetail
{
    public class GetCountryDetailQuery : IRequest<CountryDetailVm>
    {
        public Guid Id { get; set; }
    }
}
