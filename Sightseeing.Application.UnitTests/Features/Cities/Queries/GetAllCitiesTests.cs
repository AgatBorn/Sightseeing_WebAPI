using AutoMapper;
using FluentAssertions;
using Moq;
using Sightseeing.Application.Contracts.Persistence;
using Sightseeing.Application.Features.Cities.Queries.GetAllCities;
using Sightseeing.Application.Profiles;
using Sightseeing.Application.UnitTests.Mocks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sightseeing.Application.UnitTests.Features.Cities.Queries
{
    public class GetAllCitiesTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICityRepository> _mockCityRepository;

        public GetAllCitiesTests()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();

            _mockCityRepository = RepositoryMocks.GetCityRepository();
        }

        [Fact]
        public async Task Handle_ValidQuery_ShouldReturnList()
        {
            var listCount = (await _mockCityRepository.Object.GetAllAsync()).Count;

            var handler = new GetAllCitiesQueryHandler(_mapper, _mockCityRepository.Object);

            var result = await handler.Handle(new GetAllCitiesQuery(), CancellationToken.None);

            result.Should().BeOfType(typeof(CitiesListVm));
            result.Cities.Should().BeOfType(typeof(List<CityDto>));
            result.Cities.Count.Should().Be(listCount);
            result.Count.Should().Be(listCount);

            _mockCityRepository.Verify(repo => repo.GetAllAsync(), Times.Exactly(2)); // first in this test
        }
    }
}
