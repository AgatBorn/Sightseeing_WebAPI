using AutoMapper;
using FluentAssertions;
using Moq;
using Sightseeing.Application.Contracts.Persistence;
using Sightseeing.Application.Exceptions;
using Sightseeing.Application.Features.Cities.Commands.CreateCity;
using Sightseeing.Application.Profiles;
using Sightseeing.Application.UnitTests.Mocks;
using Sightseeing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sightseeing.Application.UnitTests.Features.Cities.Commands
{
    public class CreateCityTests
    {
        private readonly IMapper _mapper;

        public CreateCityTests()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldAddToRepository()
        {
            var mockCityRepository = RepositoryMocks.GetCityRepository();
            mockCityRepository.Setup(repo => repo.IsCityNameUnique(It.IsAny<string>())).ReturnsAsync(true);

            var mockCountryRepository = RepositoryMocks.GetCountryRepository();
            mockCountryRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Country {CountryId = Guid.Parse("{3ae4e108-e2df-4893-958a-2d76ab89b9dc}"), Name = "Italy"});

            var initialListCount = (await mockCityRepository.Object.GetAllAsync()).Count;

            var handler = new CreateCityCommandHandler(_mapper, mockCityRepository.Object, mockCountryRepository.Object);

            var result = await handler.Handle(new CreateCityCommand { Name = "Naples", CountryId = Guid.Parse("{3ae4e108-e2df-4893-958a-2d76ab89b9dc}") }, CancellationToken.None);

            result.Should().BeOfType(typeof(CityDto));
            result.Name.Should().Be("Naples");

            var cities = await mockCityRepository.Object.GetAllAsync();

            cities.Count.Should().Be(initialListCount + 1);

            mockCityRepository.Verify(repo => repo.AddAsync(It.IsAny<City>()), Times.Once());
        }

        [Fact]
        public void Handle_InvalidCommand_ShouldThrowValidationException()
        {
            var mockCityRepository = RepositoryMocks.GetCityRepository();
            mockCityRepository.Setup(repo => repo.IsCityNameUnique(It.IsAny<string>())).ReturnsAsync(false);

            var mockCountryRepository = RepositoryMocks.GetCountryRepository();
            mockCountryRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Country)null);

            var handler = new CreateCityCommandHandler(_mapper, mockCityRepository.Object, mockCountryRepository.Object);

            Func<Task> func = async () => await handler.Handle(new CreateCityCommand { Name = "", CountryId = Guid.Empty }, CancellationToken.None);

            func.Should().Throw<ValidationException>().Where(e => e.Errors.Count == 4 
            && e.Errors.Any(x => x.Contains("Name cannot be empty"))
            && e.Errors.Any(x => x.Contains("City with that name already exists"))
            && e.Errors.Any(x => x.Contains("Country Id cannot be empty"))
            && e.Errors.Any(x => x.Contains("Cannot find country with that id")));

            mockCityRepository.Verify(repo => repo.AddAsync(It.IsAny<City>()), Times.Never());
        }
    }
}
