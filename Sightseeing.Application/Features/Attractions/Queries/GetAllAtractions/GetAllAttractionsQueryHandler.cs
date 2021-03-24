using AutoMapper;
using MediatR;
using Sightseeing.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sightseeing.Application.Features.Attractions.Queries.GetAllAtractions
{
    public class GetAllAttractionsQueryHandler : IRequestHandler<GetAllAttractionsQuery, IList<AttractionListVm>>
    {
        private readonly IMapper _mapper;
        private readonly IAttractionRepository _attractionRepository;

        public GetAllAttractionsQueryHandler(IMapper mapper, IAttractionRepository attractionRepository)
        {
            _mapper = mapper;
            _attractionRepository = attractionRepository;
        }

        public async Task<IList<AttractionListVm>> Handle(GetAllAttractionsQuery request, CancellationToken cancellationToken)
        {
            var attractions = await _attractionRepository.GetAllWithRelatedDataAsync();

            var list = _mapper.Map<IList<AttractionListVm>>(attractions);

            return list;
        }
    }
}
