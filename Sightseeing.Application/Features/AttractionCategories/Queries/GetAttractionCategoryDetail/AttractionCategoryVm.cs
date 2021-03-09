using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Features.AttractionCategories.Queries.GetAttractionCategoryDetail
{
    public class AttractionCategoryVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<AttractionDto> Attractions { get; set; }
    }
}
