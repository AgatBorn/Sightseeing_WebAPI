using AutoMapper;
using MediatR;
using Sightseeing.Application.Contracts.Persistence;
using Sightseeing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sightseeing.Application.Features.Attractions.Commands.CreateAttraction
{
    public class CreateAttractionCommandHandler : IRequestHandler<CreateAttractionCommand, CreateAttractionCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAttractionRepository _attractionRepository;

        public CreateAttractionCommandHandler(IMapper mapper, IAttractionRepository attractionRepository)
        {
            _mapper = mapper;
            _attractionRepository = attractionRepository;
        }

        public async Task<CreateAttractionCommandResponse> Handle(CreateAttractionCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateAttractionCommandResponse();

            var attraction = _mapper.Map<Attraction>(request);

            attraction = await _attractionRepository.AddAsync(attraction);

            response.Attraction = _mapper.Map<AttractionDto>(attraction);

            return response;
        }
    }
}
