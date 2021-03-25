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

namespace Sightseeing.Application.Features.Attractions.Commands.UpdateAttraction
{
    public class UpdateAttractionCommandHandler : IRequestHandler<UpdateAttractionCommand, UpdatedAttractionDto>
    {
        private readonly IMapper _mapper;
        private readonly IAttractionRepository _attractionRepository;
        private readonly IAttractionCategoryRepository _categoryRepository;
        private readonly ICityRepository _cityRepository;

        public UpdateAttractionCommandHandler(IMapper mapper, IAttractionRepository attractionRepository, IAttractionCategoryRepository categoryRepository, ICityRepository cityRepository)
        {
            _mapper = mapper;
            _attractionRepository = attractionRepository;
            _categoryRepository = categoryRepository;
            _cityRepository = cityRepository;
        }

        public async Task<UpdatedAttractionDto> Handle(UpdateAttractionCommand request, CancellationToken cancellationToken)
        {
            var attraction = await _attractionRepository.GetByIdAsync(request.Id);

            if (attraction == null)
            {
                throw new NotFoundException(nameof(Attraction), request.Id.ToString());
            }

            var validator = new UpdateAttractionCommandValidator(_categoryRepository, _cityRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                throw new ValidationException(validationResult);
            }

            _mapper.Map(request, attraction, typeof(UpdateAttractionCommand), typeof(Attraction));

            await _attractionRepository.UpdateAsync(attraction);

            var attractionDto = _mapper.Map<UpdatedAttractionDto>(attraction);

            return attractionDto;
        }
    }
}
