using FluentValidation;
using Sightseeing.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sightseeing.Application.Features.Attractions.Commands.UpdateAttraction
{
    class UpdateAttractionCommandValidator : AbstractValidator<UpdateAttractionCommand>
    {
        private readonly IAttractionCategoryRepository _categoryRepository;
        private readonly ICityRepository _cityRepository;

        public UpdateAttractionCommandValidator(IAttractionCategoryRepository categoryRepository, ICityRepository cityRepository)
        {
            _categoryRepository = categoryRepository;
            _cityRepository = cityRepository;

            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters");

            RuleFor(a => a.AttractionCategoryId)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty")
                .MustAsync(AttractionCategoryIdExists).WithMessage($"Cannot find attraction category with that id");

            RuleFor(a => a.CityId)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty")
                .MustAsync(CityIdExists).WithMessage($"Cannot find city with that id");

            When(a => !a.IsFree, () => {
                RuleFor(a => a.Price).NotEmpty().WithMessage("{PropertyName} is required").GreaterThan(0);
            });
        }

        private async Task<bool> AttractionCategoryIdExists(Guid id, CancellationToken token)
        {
            var result = await _categoryRepository.GetByIdAsync(id);

            return result != null;
        }

        private async Task<bool> CityIdExists(Guid id, CancellationToken token)
        {
            var result = await _cityRepository.GetByIdAsync(id);

            return result != null;
        }
    }
}
