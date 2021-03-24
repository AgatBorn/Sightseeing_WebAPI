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
    public class GetAllCountriesQueryHandler : IRequestHandler<GetAllCountriesQuery, IList<CountryListVm>>
    {
        private readonly IMapper _mapper;
        private readonly ICountryRepository _countryRepository;

        public GetAllCountriesQueryHandler(IMapper mapper, ICountryRepository countryRepository)
        {
            _mapper = mapper;
            _countryRepository = countryRepository;
        }

        public async Task<IList<CountryListVm>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
        {
            var countries = await _countryRepository.GetAllAsync();

            var list = _mapper.Map<List<CountryListVm>>(countries);

            return list;
        }
    }
}
