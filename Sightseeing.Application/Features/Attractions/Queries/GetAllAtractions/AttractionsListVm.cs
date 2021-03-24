using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.Features.Attractions.Queries.GetAllAtractions
{
    public class AttractionListVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid AttractionCategoryId { get; set; }
        public string AttractionCategoryName { get; set; }
        public Guid CityId { get; set; }
        public string CityName { get; set; }
    }
}
