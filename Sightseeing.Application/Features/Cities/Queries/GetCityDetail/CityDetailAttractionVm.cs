using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Features.Cities.Queries.GetCityDetail
{
    public class CityDetailAttractionVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public Guid AttractionCategoryId { get; set; }
        public string AttractionCategoryName { get; set; }
        public bool IsFree { get; set; }
        public int? Price { get; set; }
    }
}
