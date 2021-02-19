using AutoMapper;
using MediatR;
using Sightseeing.Application.Contracts.Persistence;
using Sightseeing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sightseeing.Application.Features.Countries.Commands
{
    public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, CreateCountryCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICountryRepository _countryRepository;

        public CreateCountryCommandHandler(IMapper mapper, ICountryRepository countryRepository)
        {
            _mapper = mapper;
            _countryRepository = countryRepository;
        }

        public async Task<CreateCountryCommandResponse> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateCountryCommandResponse();

            var validator = new CreateCountryCommandValidator(_countryRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                response.Success = false;

                response.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    response.ValidationErrors.Add(error.ErrorMessage);
                }
            }
            else
            {
                var country = _mapper.Map<Country>(request);
                country = await _countryRepository.AddAsync(country);

                var countryDto = _mapper.Map<CountryDto>(country);
                response.Country = countryDto;
            }

            return response;
        }
    }
}
