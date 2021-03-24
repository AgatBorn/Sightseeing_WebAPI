using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Features.AttractionCategories.Queries.GetAttractionCategoryDetail
{
    public class AttractionCategoryDetailAttractionVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public bool IsFree { get; set; }
        public int? Price { get; set; }
        public Guid CityId { get; set; }
        public string CityName { get; set; }
    }
}
