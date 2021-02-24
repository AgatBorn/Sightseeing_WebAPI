using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Features.AttractionCategories.Commands.CreateAttractionCategory
{
    public class CreateAttractionCategoryCommandValidator : AbstractValidator<CreateAttractionCategoryCommand>
    {
        public CreateAttractionCategoryCommandValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
        }
    }
}
