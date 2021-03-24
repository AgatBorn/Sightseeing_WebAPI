using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Features.Attractions.Queries.GetAllAtractions
{
    public class GetAllAttractionsQuery : IRequest<IList<AttractionListVm>>
    {
    }
}
