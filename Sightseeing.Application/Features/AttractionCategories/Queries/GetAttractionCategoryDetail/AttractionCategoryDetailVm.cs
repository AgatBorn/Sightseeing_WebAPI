using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Features.AttractionCategories.Queries.GetAttractionCategoryDetail
{
    public class AttractionCategoryDetailVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<AttractionCategoryDetailAttractionVm> Attractions { get; set; }
    }
}
