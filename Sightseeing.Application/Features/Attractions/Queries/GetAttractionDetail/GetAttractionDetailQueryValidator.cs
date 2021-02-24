using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Features.Attractions.Queries.GetAttractionDetail
{
    public class GetAttractionDetailQueryValidator : AbstractValidator<GetAttractionDetailQuery>
    {
        public GetAttractionDetailQueryValidator()
        {
            RuleFor(a => a.Id)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty");
        }
    }
}
