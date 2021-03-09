using AutoMapper;
using MediatR;
using Sightseeing.Application.Contracts.Persistence;
using Sightseeing.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sightseeing.Application.Features.Cities.Queries.GetCityDetail
{
    public class GetCityDetailQueryHandler : IRequestHandler<GetCityDetailQuery, CityDetailVm>
    {
        private readonly IMapper _mapper;
        private readonly ICityRepository _cityRepository;

        public GetCityDetailQueryHandler(IMapper mapper, ICityRepository cityRepository)
        {
            _mapper = mapper;
            _cityRepository = cityRepository;
        }

        public async Task<CityDetailVm> Handle(GetCityDetailQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetCityDetailQueryValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                throw new ValidationException(validationResult);
            }

            var city = await _cityRepository.GetByIdWithRelatedDataAsync(request.Id);

            if (city == null)
            {
                throw new ApplicationException($"Attraction {request.Id} not found");
            }

            var cityDetailVm = _mapper.Map<CityDetailVm>(city);

            return cityDetailVm;
        }
    }
}
