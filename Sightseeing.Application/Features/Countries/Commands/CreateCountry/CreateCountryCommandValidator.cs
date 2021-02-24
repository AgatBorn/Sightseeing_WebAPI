using FluentValidation;
using Sightseeing.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sightseeing.Application.Features.Countries.Commands.CreateCountry
{
    public class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
    {
        private readonly ICountryRepository _countryRepository;

        public CreateCountryCommandValidator(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty")
                .MustAsync(CountryNameUnique).WithMessage($"Country with that name already exists");
        }

        private async Task<bool> CountryNameUnique(string name, CancellationToken token)
        {
            return await _countryRepository.IsCountryNameUnique(name);
        }
    }
}
