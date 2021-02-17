using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Features.Attractions.Queries.GetAttractionDetail
{
    public class GetAttractionDetailQuery : IRequest<AttractionDetailVm>
    {
        public Guid Id { get; set; }
    }
}
