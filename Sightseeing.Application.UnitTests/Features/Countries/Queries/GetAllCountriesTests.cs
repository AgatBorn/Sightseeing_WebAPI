using AutoMapper;
using FluentAssertions;
using Moq;
using Sightseeing.Application.Contracts.Persistence;
using Sightseeing.Application.Features.Countries.Queries.GetAllCountries;
using Sightseeing.Application.Profiles;
using Sightseeing.Application.UnitTests.Mocks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;


namespace Sightseeing.Application.UnitTests.Features.Countries.Queries
{
    public class GetAllCountriesTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICountryRepository> _mockCountryRepository;

        public GetAllCountriesTests()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();

            _mockCountryRepository = RepositoryMocks.GetCountryRepository();
        }

        [Fact]
        public async Task Handle_ValidQuery_ShouldReturnList()
        {
            var listCount = (await _mockCountryRepository.Object.GetAllAsync()).Count;

            var handler = new GetAllCountriesQueryHandler(_mapper, _mockCountryRepository.Object);

            var result = await handler.Handle(new GetAllCountriesQuery(), CancellationToken.None);

            result.Should().BeOfType(typeof(List<CountryListVm>));
            result.Count.Should().Be(listCount);

            _mockCountryRepository.Verify(repo => repo.GetAllAsync(), Times.Exactly(2)); // first in this test
        }
    }
}
