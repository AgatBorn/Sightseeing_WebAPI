using FluentValidation;
using Sightseeing.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sightseeing.Application.Features.Cities.Commands.CreateCity
{
    public class CreateCityCommandValidator : AbstractValidator<CreateCityCommand>
    {
        private readonly ICityRepository _cityRepository;
        private readonly ICountryRepository _countryRepository;

        public CreateCityCommandValidator(ICityRepository cityRepository, ICountryRepository countryRepository)
        {
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.")
                .MustAsync(CityNameUnique).WithMessage($"City with that name already exists");

            RuleFor(c => c.CountryId)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty")
                .MustAsync(CountryIdExists).WithMessage($"Cannot find country with that id");
        }

        private async Task<bool> CityNameUnique(string name, CancellationToken token)
        {
            return await _cityRepository.IsCityNameUnique(name);
        }
        private async Task<bool> CountryIdExists(Guid countryId, CancellationToken token)
        {
            var country = await _countryRepository.GetByIdAsync(countryId);

            var exists = country != null;

            return await Task.FromResult(exists);
        }
    }
}
