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

namespace Sightseeing.Application.Features.Cities.Commands.CreateCity
{
    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, CityDto>
    {
        private readonly IMapper _mapper;
        private readonly ICityRepository _cityRepository;
        private readonly ICountryRepository _countryRepository;

        public CreateCityCommandHandler(IMapper mapper, ICityRepository cityRepository, ICountryRepository countryRepository)
        {
            _mapper = mapper;
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
        }

        public async Task<CityDto> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateCityCommandValidator(_cityRepository, _countryRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                throw new ValidationException(validationResult);
            }

            var city = _mapper.Map<City>(request);
            city = await _cityRepository.AddAsync(city);

            var cityDto = _mapper.Map<CityDto>(city);

            return cityDto;
        }
    }
}
