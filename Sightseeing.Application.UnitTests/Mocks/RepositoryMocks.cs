using Moq;
using Sightseeing.Application.Contracts.Persistence;
using Sightseeing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sightseeing.Application.UnitTests.Mocks
{
    public static class RepositoryMocks
    {
        public static Mock<IAttractionCategoryRepository> GetAttractionCategoryRepository()
        {
            var gardenGuid = Guid.Parse("{3ae4e108-e2df-4893-958a-2d76ab89b9dc}");
            var castleGuid = Guid.Parse("{513fb459-ab65-4858-98ab-6424806be77e}");
            var museumGuid = Guid.Parse("{21d106af-0880-4eae-aa98-e8a5957a29c6}");

            var categories = new List<AttractionCategory>
            {
                new AttractionCategory
                {
                    AttractionCategoryId = gardenGuid,
                    Name = "Garden"
                },
                new AttractionCategory
                {
                    AttractionCategoryId = castleGuid,
                    Name = "Castle"
                },
                new AttractionCategory
                {
                    AttractionCategoryId = museumGuid,
                    Name = "Museum"
                }
            };

            var mockAttractionCategoryRepository = new Mock<IAttractionCategoryRepository>();

            mockAttractionCategoryRepository.Setup(repo => repo.GetByIdWithRelatedDataAsync(It.IsAny<Guid>())).ReturnsAsync(categories[0]);
            mockAttractionCategoryRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(categories);
            mockAttractionCategoryRepository.Setup(repo => repo.AddAsync(It.IsAny<AttractionCategory>())).ReturnsAsync((AttractionCategory category) =>
                {
                    categories.Add(category);
                    return category;
                });

            return mockAttractionCategoryRepository;
        }
    
        public static Mock<IAttractionRepository> GetAttractionRepository()
        {
            var pantheon = new Attraction
            {
                AttractionId = Guid.Parse("{3ae4e108-e2df-4893-958a-2d76ab89b9dc}"),
                Name = "Pantheon",
                Date = "113–125 AD",
                Author = "Apollodoros",
                Description = "The building is cylindrical with a portico of large granite Corinthian columns (eight in the first rank and two groups of four behind) under a pediment.",
                AttractionCategoryId = Guid.Parse("{513fb459-ab65-4858-98ab-6424806be77e}"),
                Category = new AttractionCategory
                {
                    AttractionCategoryId = Guid.Parse("{513fb459-ab65-4858-98ab-6424806be77e}"),
                    Name = "Temple"
                },
                IsFree = true,
                CityId = Guid.Parse("{21d106af-0880-4eae-aa98-e8a5957a29c6}"),
                City = new City
                {
                    CityId = Guid.Parse("{21d106af-0880-4eae-aa98-e8a5957a29c6}"),
                    Name = "Rome"
                }
            };

            var treviFountain = new Attraction
            {
                AttractionId = Guid.Parse("{b5b45a69-d868-4a53-82b2-4add85930e30}"),
                Name = "Trevi Fountain",
                Date = "1735 - 1776",
                Author = "Nicola Salvi",
                Description = "It is the largest Baroque fountain in the city and one of the most famous fountains in the world. ",
                AttractionCategoryId = Guid.Parse("{85ffecfe-1ab7-415d-942f-23247377356c}"),
                Category = new AttractionCategory
                {
                    AttractionCategoryId = Guid.Parse("{85ffecfe-1ab7-415d-942f-23247377356c}"),
                    Name = "Fountain"
                },
                IsFree = true,
                CityId = Guid.Parse("{21d106af-0880-4eae-aa98-e8a5957a29c6}"),
                City = new City
                {
                    CityId = Guid.Parse("{21d106af-0880-4eae-aa98-e8a5957a29c6}"),
                    Name = "Rome"
                }
            };

            var attractions = new List<Attraction> { pantheon, treviFountain };

            var mockAttractionRepository = new Mock<IAttractionRepository>();

            mockAttractionRepository.Setup(repo => repo.GetByIdWithRelatedDataAsync(It.IsAny<Guid>())).ReturnsAsync(pantheon);
            mockAttractionRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(attractions);
            mockAttractionRepository.Setup(repo => repo.AddAsync(It.IsAny<Attraction>())).ReturnsAsync((Attraction attraction) =>
            {
                attractions.Add(attraction);
                return attraction;
            });

            return mockAttractionRepository;
        }

        public static Mock<ICityRepository> GetCityRepository()
        {
            var romeGuid = Guid.Parse("{3ae4e108-e2df-4893-958a-2d76ab89b9dc}");
            var florenceGuid = Guid.Parse("{513fb459-ab65-4858-98ab-6424806be77e}");
            var milanGuid = Guid.Parse("{21d106af-0880-4eae-aa98-e8a5957a29c6}");

            var cities = new List<City>
            {
                new City
                {
                    CityId = romeGuid,
                    Name = "Rome"
                },
                new City
                {
                    CityId = florenceGuid,
                    Name = "Florence"
                },
                new City
                {
                    CityId = milanGuid,
                    Name = "Milan"
                }
            };

            var mockCityRepository = new Mock<ICityRepository>();

            mockCityRepository.Setup(c => c.GetByIdWithRelatedDataAsync(It.IsAny<Guid>())).ReturnsAsync(cities[0]);
            mockCityRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(cities);
            mockCityRepository.Setup(repo => repo.AddAsync(It.IsAny<City>())).ReturnsAsync((City city) =>
            {
                cities.Add(city);
                return city;
            });

            return mockCityRepository;
        }

        public static Mock<ICountryRepository> GetCountryRepository()
        {
            var italyGuid = Guid.Parse("{3ae4e108-e2df-4893-958a-2d76ab89b9dc}");
            var germanyGuid = Guid.Parse("{513fb459-ab65-4858-98ab-6424806be77e}");
            var polandGuid = Guid.Parse("{21d106af-0880-4eae-aa98-e8a5957a29c6}");

            var countries = new List<Country>
            {
                new Country
                {
                    CountryId = italyGuid,
                    Name = "Italy"
                },
                new Country
                {
                    CountryId = germanyGuid,
                    Name = "Germany"
                },
                new Country
                {
                    CountryId = polandGuid,
                    Name = "Poland"
                }
            };

            var mockCountryRepository = new Mock<ICountryRepository>();

            mockCountryRepository.Setup(c => c.GetByIdWithRelatedDataAsync(It.IsAny<Guid>())).ReturnsAsync(countries[0]);
            mockCountryRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(countries);
            mockCountryRepository.Setup(repo => repo.AddAsync(It.IsAny<Country>())).ReturnsAsync((Country country) =>
            {
                countries.Add(country);
                return country;
            });

            return mockCountryRepository;
        }
    }
}
