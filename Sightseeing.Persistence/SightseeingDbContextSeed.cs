using Microsoft.EntityFrameworkCore;
using Sightseeing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sightseeing.Persistence
{
    public class SightseeingDbContextSeed
    {
        public static async Task SeedAsync(SightseeingDbContext dbContext)
        {
            await AddCountries(dbContext);
            await AddCities(dbContext);
            await AddCategories(dbContext);
            await AddAttractions(dbContext);
        }

        private static async Task AddCountries(SightseeingDbContext context)
        {
            if (await context.Countries.AnyAsync())
            {
                return;
            }

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
            await context.SaveChangesAsync();
        }

        private static async Task AddCities(SightseeingDbContext context)
        {
            if (await context.Cities.AnyAsync())
            {
                return;
            }

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
            await context.SaveChangesAsync();
        }

        private static async Task AddCategories(SightseeingDbContext context)
        {
            if (await context.AttractionCategories.AnyAsync())
            {
                return;
            }

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

            var amphitheatre = new AttractionCategory
            {
                AttractionCategoryId = Guid.Parse("{720ba5a2-0faf-4c2c-9d19-8c56da17e62b}"),
                Name = "Amphitheatre"
            };

            var palace = new AttractionCategory
            {
                AttractionCategoryId = Guid.Parse("{bbc05cbc-fdd1-41df-8bfd-f662c8663858}"),
                Name = "Palace"
            };

            context.AttractionCategories.AddRange(museum, castle, cathedral, temple, amphitheatre, palace);
            await context.SaveChangesAsync();
        }

        private static async Task AddAttractions(SightseeingDbContext context)
        {
            if (await context.Attractions.AnyAsync())
            {
                return;
            }

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
                CityId = Guid.Parse("{0816c8ec-8178-408f-8aa3-47cfeca4ff28}")
            };

            var cathedral = new Attraction
            {
                AttractionId = Guid.Parse("{80f20e38-6a81-4670-ad7a-b98ea83d2aa1}"),
                Name = "Milan Cathedral",
                Author = "Simone da Orsenigo",
                Date = "1386",
                Description = "The cathedral church of Milan, Lombardy, Italy. Dedicated to the Nativity of St Mary (Santa Maria Nascente), it is the seat of the Archbishop of Milan, currently Archbishop Mario Delpini.",
                AttractionCategoryId = Guid.Parse("{6909101a-5a1f-433a-b2db-11038e4ffff7}"),
                IsFree = true,
                CityId = Guid.Parse("{54210db9-2f35-482e-9254-90c38e5aa684}")
            };

            var colosseum = new Attraction
            {
                AttractionId = Guid.Parse("{b59c4cac-6ba9-4dec-9309-c79c4ce1c4af}"),
                Name = "Colosseum",
                Author = "Vespasian, Titus",
                Date = "70 - 80",
                Description = "An oval amphitheatre in the centre of the city of Rome, Italy, just east of the Roman Forum and is the largest ancient amphitheatre ever built, and is still the largest standing amphitheater in the world today, despite its age.",
                AttractionCategoryId = Guid.Parse("{720ba5a2-0faf-4c2c-9d19-8c56da17e62b}"),
                IsFree = false,
                Price = 20,
                CityId = Guid.Parse("{b3ec95dd-c424-4a2b-a329-50e6bdfff8b8}")
            };

            var quirinal = new Attraction
            {
                AttractionId = Guid.Parse("{53c493ff-ead8-4c9f-985f-4e34ae5b0fa2}"),
                Name = "Quirinal Palace",
                Author = "Domenico Fontana, Carlo Maderno",
                Date = "1583",
                Description = "A historic building in Rome, Italy, one of the three current official residences of the President of the Italian Republic, together with Villa Rosebery in Naples and the Tenuta di Castelporziano, an estate on the outskirts of Rome, some 25 km from the centre of the city. It is located on the Quirinal Hill, the highest of the seven hills of Rome in an area colloquially called Monte Cavallo. It has served as the residence for thirty Popes, four Kings of Italy and twelve Presidents of the Italian Republic.",
                AttractionCategoryId = Guid.Parse("{bbc05cbc-fdd1-41df-8bfd-f662c8663858}"),
                IsFree = true,
                CityId = Guid.Parse("{b3ec95dd-c424-4a2b-a329-50e6bdfff8b8}")
            };

            var palazzoBarberini = new Attraction
            {
                AttractionId = Guid.Parse("{69775648-dd97-4411-bf7f-a17f9edc92ba}"),
                Name = "Palazzo Barberini",
                Author = "Gian Lorenzo Bernini, Carlo Maderno, Francesco Borromini",
                Date = "1625 -1633",
                Description = "A 17th-century palace in Rome, facing the Piazza Barberini in Rione Trevi. Today it houses the Galleria Nazionale d'Arte Antica, the main national collection of older paintings in Rome.",
                AttractionCategoryId = Guid.Parse("{bbc05cbc-fdd1-41df-8bfd-f662c8663858}"),
                IsFree = true,
                CityId = Guid.Parse("{b3ec95dd-c424-4a2b-a329-50e6bdfff8b8}")
            };

            var pantheon = new Attraction
            {
                AttractionId = Guid.Parse("{1f517b1b-e1b1-44f9-a9e8-f8d1d9dc536c}"),
                Name = "Pantheon",
                Author = "Trajan, Hadrian",
                Date = "113–125",
                Description = "A former Roman temple, now a Catholic church (Basilica di Santa Maria ad Martyres or Basilica of St. Mary and the Martyrs), in Rome, Italy, on the site of an earlier temple commissioned by Marcus Agrippa during the reign of Augustus (27 BC – 14 AD). It was rebuilt by the emperor Hadrian and probably dedicated around 126AD.",
                AttractionCategoryId = Guid.Parse("{7e12db9e-5648-4385-a9dc-abe24f1fbc4b}"),
                IsFree = true,
                CityId = Guid.Parse("{b3ec95dd-c424-4a2b-a329-50e6bdfff8b8}")
            };

            context.Attractions.AddRange(museum, castle, cathedral, colosseum, quirinal, palazzoBarberini, pantheon);
            await context.SaveChangesAsync();
        }
    }
}
