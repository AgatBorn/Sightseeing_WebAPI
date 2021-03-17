using Newtonsoft.Json;
using Sightseeing.Domain.Entities;
using Sightseeing.Persistence;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;

namespace Sightseeing.Api.IntegrationTests.Common
{
    public static class Utilities
    {
        internal static StringContent GetRequestContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(stringResponse);

            return result;
        }

        internal static void InitializeDbForTests(SightseeingDbContext context)
        {
            AddCountries(context);
            AddCities(context);
            AddCategories(context);
            AddAttractions(context);
        }

        private static void AddCountries(SightseeingDbContext context)
        {
            var italy = new Country
            {
                CountryId = Guid.Parse("{3ae4e108-e2df-4893-958a-2d76ab89b9dc}"),
                Name = "Italy"
            };

            var germany = new Country
            {
                CountryId = Guid.Parse("{513fb459-ab65-4858-98ab-6424806be77e}"),
                Name = "Germany"
            };

            var poland = new Country
            {
                CountryId = Guid.Parse("{21d106af-0880-4eae-aa98-e8a5957a29c6}"),
                Name = "Poland"
            };

            context.Countries.AddRange(italy, germany, poland);
            context.SaveChanges();
        }

        private static void AddCities(SightseeingDbContext context)
        {
            var milan = new City
            {
                CityId = Guid.Parse("{54210db9-2f35-482e-9254-90c38e5aa684}"),
                Name = "Milan",
                CountryId = Guid.Parse("{3ae4e108-e2df-4893-958a-2d76ab89b9dc}")
            };

            var rome = new City
            {
                CityId = Guid.Parse("{b3ec95dd-c424-4a2b-a329-50e6bdfff8b8}"),
                Name = "Rome",
                CountryId = Guid.Parse("{3ae4e108-e2df-4893-958a-2d76ab89b9dc}")
            };

            var berlin = new City
            {
                CityId = Guid.Parse("{6dc2edef-51d2-4411-b5d2-d32e4b3609be}"),
                Name = "Berlin",
                CountryId = Guid.Parse("{513fb459-ab65-4858-98ab-6424806be77e}")
            };

            var szczecin = new City
            {
                CityId = Guid.Parse("{0816c8ec-8178-408f-8aa3-47cfeca4ff28}"),
                Name = "Szczecin",
                CountryId = Guid.Parse("{21d106af-0880-4eae-aa98-e8a5957a29c6}")
            };

            context.Cities.AddRange(milan, rome, berlin, szczecin);
            context.SaveChanges();
        }

        private static void AddCategories(SightseeingDbContext context)
        {
            var museum = new AttractionCategory
            {
                AttractionCategoryId = Guid.Parse("{24a31ba7-4030-48ed-a705-c5584f7ff860}"),
                Name = "Museum"
            };

            var castle = new AttractionCategory
            {
                AttractionCategoryId = Guid.Parse("{b3cbceac-72ab-4c12-9d36-2292a33265f8}"),
                Name = "Castle"
            };

            var cathedral = new AttractionCategory
            {
                AttractionCategoryId = Guid.Parse("{6909101a-5a1f-433a-b2db-11038e4ffff7}"),
                Name = "Cathedral"
            };

            var temple = new AttractionCategory
            {
                AttractionCategoryId = Guid.Parse("{7e12db9e-5648-4385-a9dc-abe24f1fbc4b}"),
                Name = "Temple"
            };

            context.AttractionCategories.AddRange(museum, castle, cathedral, temple);
            context.SaveChanges();
        }

        private static void AddAttractions(SightseeingDbContext context)
        {
            var museum = new Attraction
            {
                AttractionId = Guid.Parse("{9397df36-b4a1-4658-9c08-59b3e8d8dcf1}"),
                Name = "Pergamon Museum",
                Author = "Alfred Messel",
                Date = "1910",
                Description = "A listed building on the Museum Island in the historic centre of Berlin and part of the UNESCO World Heritage.",
                AttractionCategoryId = Guid.Parse("{24a31ba7-4030-48ed-a705-c5584f7ff860}"),
                IsFree = false,
                Price = 10,
                CityId = Guid.Parse("{6dc2edef-51d2-4411-b5d2-d32e4b3609be}")
            };

            var castle = new Attraction
            {
                AttractionId = Guid.Parse("{d05c0308-582e-4042-800c-9248e7bb0723}"),
                Name = "Ducal Castle",
                Author = "Wilhelm Zachariasz Italus",
                Date = "1346-1428/1944",
                Description = "Castle was the seat of the dukes of Pomerania-Stettin of the House of Pomerania (Griffins), who ruled the Duchy of Pomerania from 1121 to 1637.",
                AttractionCategoryId = Guid.Parse("{b3cbceac-72ab-4c12-9d36-2292a33265f8}"),
                IsFree = true,
                CityId = Guid.Parse("{21d106af-0880-4eae-aa98-e8a5957a29c6}")
            };

            var cathedral = new Attraction
            {
                AttractionId = Guid.Parse("{80f20e38-6a81-4670-ad7a-b98ea83d2aa1}"),
                Name = "Milan Cathedral",
                Author = "Simone da Orsenigo",
                Date = "1386",
                Description = "the cathedral church of Milan, Lombardy, Italy. Dedicated to the Nativity of St Mary (Santa Maria Nascente), it is the seat of the Archbishop of Milan, currently Archbishop Mario Delpini.",
                AttractionCategoryId = Guid.Parse("{6909101a-5a1f-433a-b2db-11038e4ffff7}"),
                IsFree = true,
                CityId = Guid.Parse("{54210db9-2f35-482e-9254-90c38e5aa684}")
            };

            context.Attractions.AddRange(museum, castle, cathedral);
            context.SaveChanges();
        }
    }
}
