using AutoMapper;
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
        private readonly IAttractionCategoryRepository _categoryRepository;
        private readonly ICityRepository _cityRepository;

        public CreateAttractionCommandHandler(IMapper mapper, IAttractionRepository attractionRepository, IAttractionCategoryRepository categoryRepository, ICityRepository cityRepository)
        {
            _mapper = mapper;
            _attractionRepository = attractionRepository;
            _categoryRepository = categoryRepository;
            _cityRepository = cityRepository;
        }

        public async Task<AttractionDto> Handle(CreateAttractionCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateAttractionCommandValidator(_categoryRepository, _cityRepository);
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
