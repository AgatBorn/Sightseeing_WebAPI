using AutoMapper;
using MediatR;
using Sightseeing.Application.Contracts.Persistence;
using Sightseeing.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sightseeing.Application.Features.Countries.Queries.GetCountryDetail
{
    public class GetCountryDetailQueryHandler : IRequestHandler<GetCountryDetailQuery, CountryDetailVm>
    {
        private readonly IMapper _mapper;
        private readonly ICountryRepository _countryRepository;

        public GetCountryDetailQueryHandler(IMapper mapper, ICountryRepository countryRepository)
        {
            _mapper = mapper;
            _countryRepository = countryRepository;
        }

        public async Task<CountryDetailVm> Handle(GetCountryDetailQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetCountryDetailQueryValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                throw new ValidationException(validationResult);
            }

            var city = await _countryRepository.GetByIdWithRelatedDataAsync(request.Id);

            if (city == null)
            {
                throw new ApplicationException($"Country {request.Id} not found");
            }

            var cityDetailVm = _mapper.Map<CountryDetailVm>(city);

            return cityDetailVm;
        }
    }
}
