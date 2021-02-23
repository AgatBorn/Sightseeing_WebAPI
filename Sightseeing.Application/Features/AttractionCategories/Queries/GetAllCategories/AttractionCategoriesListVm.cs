using System.Collections.Generic;

namespace Sightseeing.Application.Features.AttractionCategories.Queries.GetAllCategories
{
    public class AttractionCategoriesListVm
    {
        public IList<AttractionCategoryDto> Categories { get; set; }
        public int Count { get; set; }
    }
}