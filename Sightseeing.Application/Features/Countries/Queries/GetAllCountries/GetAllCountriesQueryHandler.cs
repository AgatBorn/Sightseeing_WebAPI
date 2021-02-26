using AutoMapper;
using MediatR;
using Sightseeing.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sightseeing.Application.Features.Countries.Queries.GetAllCountries
{
    public class GetAllCountriesQueryHandler : IRequestHandler<GetAllCountriesQuery, CountriesListVm>
    {
        private readonly IMapper _mapper;
        private readonly ICountryRepository _countryRepository;

        public GetAllCountriesQueryHandler(IMapper mapper, ICountryRepository countryRepository)
        {
            _mapper = mapper;
            _countryRepository = countryRepository;
        }

        public async Task<CountriesListVm> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
        {
            var countries = await _countryRepository.GetAllAsync();

            var countriesDto = _mapper.Map<List<CountryDto>>(countries);

            var vm = new CountriesListVm
            {
                Countries = countriesDto,
                Count = countriesDto.Count
            };

            return vm;
        }
    }
}
