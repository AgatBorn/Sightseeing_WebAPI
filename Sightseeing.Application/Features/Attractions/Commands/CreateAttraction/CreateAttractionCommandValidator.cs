using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Features.Attractions.Commands.CreateAttraction
{
    public class CreateAttractionCommandValidator : AbstractValidator<CreateAttractionCommand>
    {
        public CreateAttractionCommandValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(a => a.AttractionCategoryId)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty")
                .NotNull();

            RuleFor(a => a.CityId)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty")
                .NotNull();

            When(a => !a.IsFree, () => {
                RuleFor(a => a.Price).NotEmpty().WithMessage("{PropertyName} is required.").GreaterThan(0);
            });
        }
    }
}
