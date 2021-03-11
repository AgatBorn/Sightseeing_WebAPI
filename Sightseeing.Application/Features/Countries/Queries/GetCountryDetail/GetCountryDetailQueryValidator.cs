using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Features.Countries.Queries.GetCountryDetail
{
    public class GetCountryDetailQueryValidator : AbstractValidator<GetCountryDetailQuery>
    {
        public GetCountryDetailQueryValidator()
        {
            RuleFor(a => a.Id)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty");
        }
    }
}
