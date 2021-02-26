using AutoMapper;
using MediatR;
using Sightseeing.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sightseeing.Application.Features.Cities.Queries.GetAllCities
{
    public class GetAllCitiesQueryHandler : IRequestHandler<GetAllCitiesQuery, CitiesListVm>
    {
        private readonly IMapper _mapper;
        private readonly ICityRepository _cityRepository;

        public GetAllCitiesQueryHandler(IMapper mapper, ICityRepository cityRepository)
        {
            _mapper = mapper;
            _cityRepository = cityRepository;
        }

        public async Task<CitiesListVm> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            var cities = await _cityRepository.GetAllAsync();

            var citiesDto = _mapper.Map<List<CityDto>>(cities);

            var vm = new CitiesListVm
            {
                Cities = citiesDto,
                Count = citiesDto.Count
            };

            return vm;
        }
    }
}
