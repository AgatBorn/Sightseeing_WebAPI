using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Features.AttractionCategories.Queries.GetAllCategories
{
    public class GetAllAttractionCategoriesQuery : IRequest<AttractionCategoriesListVm>
    {
    }
}
