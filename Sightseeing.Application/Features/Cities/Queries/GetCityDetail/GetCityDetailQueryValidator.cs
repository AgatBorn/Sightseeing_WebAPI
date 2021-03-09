using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Features.Cities.Queries.GetCityDetail
{
    public class GetCityDetailQueryValidator : AbstractValidator<GetCityDetailQuery>
    {
        public GetCityDetailQueryValidator()
        {
            RuleFor(a => a.Id)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty");
        }
    }
}
