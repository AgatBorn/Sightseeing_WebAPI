using AutoMapper;
using FluentAssertions;
using Moq;
using Sightseeing.Application.Contracts.Persistence;
using Sightseeing.Application.Exceptions;
using Sightseeing.Application.Features.Cities.Queries.GetCityDetail;
using Sightseeing.Application.Profiles;
using Sightseeing.Application.UnitTests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sightseeing.Application.UnitTests.Features.Cities.Queries
{
    public class GetCityDetailTests
    {
        private IMapper _mapper;
        private Mock<ICityRepository> _mockCityRepository;

        public GetCityDetailTests()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();

            _mockCityRepository = RepositoryMocks.GetCityRepository();
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldGetTheDetails()
        {
            var handler = new GetCityDetailQueryHandler(_mapper, _mockCityRepository.Object);

            var command = new GetCityDetailQuery { Id = Guid.Parse("{3ae4e108-e2df-4893-958a-2d76ab89b9dc}") };

            var attractionResult = await handler.Handle(command, CancellationToken.None);

            attractionResult.Should().BeOfType(typeof(CityDetailVm));
            attractionResult.Name.Should().Be("Rome");

            _mockCityRepository.Verify(repo => repo.GetByIdWithRelatedDataAsync(It.IsAny<Guid>()), Times.Once());
        }

        [Fact]
        public void Handle_InvalidCommand_ShouldThrowValidationException()
        {
            var handler = new GetCityDetailQueryHandler(_mapper, _mockCityRepository.Object);

            Func<Task> func = async () => await handler.Handle(new GetCityDetailQuery(), CancellationToken.None);

            func.Should().Throw<ValidationException>().Where(e => e.Errors.Count == 1 && e.Errors.Any(x => x.Contains("Id cannot be empty")));

            _mockCityRepository.Verify(repo => repo.GetByIdWithRelatedDataAsync(It.IsAny<Guid>()), Times.Never());
        }
    }
}
