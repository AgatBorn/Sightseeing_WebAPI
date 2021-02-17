using AutoMapper;
using MediatR;
using Sightseeing.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sightseeing.Application.Features.Attractions.Queries.GetAttractionDetail
{
    public class GetAttractionDetailQueryHandler : IRequestHandler<GetAttractionDetailQuery, AttractionDetailVm>
    {
        private readonly IMapper _mapper;
        private readonly IAttractionRepository _attractionRepository;

        public GetAttractionDetailQueryHandler(IMapper mapper, IAttractionRepository attractionRepository)
        {
            _mapper = mapper;
            _attractionRepository = attractionRepository;
        }

        public async Task<AttractionDetailVm> Handle(GetAttractionDetailQuery request, CancellationToken cancellationToken)
        {
            var attraction = await _attractionRepository.GetByIdWithRelatedDataAsync(request.Id);

            if (attraction == null)
            {
                throw new ApplicationException($"Attraction {request.Id} not found");
            }

            var attractionDetailDto = _mapper.Map<AttractionDetailVm>(attraction);

            return attractionDetailDto;
        }
    }
}
