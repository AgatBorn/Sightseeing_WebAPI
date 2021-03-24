using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Features.AttractionCategories.Queries.GetAttractionCategoryDetail
{
    public class GetAttractionCategoryDetailQuery : IRequest<AttractionCategoryDetailVm>
    {
        public Guid Id { get; set; }
    }
}
