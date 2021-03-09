using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Features.AttractionCategories.Queries.GetAttractionCategoryDetail
{
    public class GetAttractionCategoryDetailQueryValidator : AbstractValidator<GetAttractionCategoryDetailQuery>
    {
        public GetAttractionCategoryDetailQueryValidator()
        {
            RuleFor(a => a.Id)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty");
        }
    }
}
