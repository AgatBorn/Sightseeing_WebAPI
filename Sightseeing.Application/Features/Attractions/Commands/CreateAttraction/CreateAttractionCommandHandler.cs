﻿using AutoMapper;
using MediatR;
using Sightseeing.Application.Contracts.Persistence;
using Sightseeing.Application.Exceptions;
using Sightseeing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sightseeing.Application.Features.Attractions.Commands.CreateAttraction
{
    public class CreateAttractionCommandHandler : IRequestHandler<CreateAttractionCommand, AttractionDto>
    {
        private readonly IMapper _mapper;
        private readonly IAttractionRepository _attractionRepository;

        public CreateAttractionCommandHandler(IMapper mapper, IAttractionRepository attractionRepository)
        {
            _mapper = mapper;
            _attractionRepository = attractionRepository;
        }

        public async Task<AttractionDto> Handle(CreateAttractionCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateAttractionCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                throw new ValidationException(validationResult);
            }

            var attraction = _mapper.Map<Attraction>(request);

            attraction = await _attractionRepository.AddAsync(attraction);

            var attractionDto = _mapper.Map<AttractionDto>(attraction);

            return attractionDto;
        }
    }
}
