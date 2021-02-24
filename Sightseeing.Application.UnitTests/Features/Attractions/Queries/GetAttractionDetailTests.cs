using AutoMapper;
using FluentAssertions;
using Moq;
using Sightseeing.Application.Contracts.Persistence;
using Sightseeing.Application.Exceptions;
using Sightseeing.Application.Features.Attractions.Queries.GetAttractionDetail;
using Sightseeing.Application.Profiles;
using Sightseeing.Application.UnitTests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sightseeing.Application.UnitTests.Features.Attractions.Queries
{
    public class GetAttractionDetailTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAttractionRepository> _mockAttractionRepository;

        public GetAttractionDetailTests()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();

            _mockAttractionRepository = RepositoryMocks.GetAttractionRepository();
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldAddToRepository()
        {
            var handler = new GetAttractionDetailQueryHandler(_mapper, _mockAttractionRepository.Object);

            var command = new GetAttractionDetailQuery { Id = Guid.Parse("{3ae4e108-e2df-4893-958a-2d76ab89b9dc}") };

            var attractionResult = await handler.Handle(command, CancellationToken.None);

            attractionResult.Should().BeOfType(typeof(AttractionDetailVm));
            attractionResult.Name.Should().Be("Pantheon");
            attractionResult.Date.Should().Be("113–125 AD");
            attractionResult.Author.Should().Be("Apollodoros");
            attractionResult.Description.Should().Be("The building is cylindrical with a portico of large granite Corinthian columns (eight in the first rank and two groups of four behind) under a pediment.");
            attractionResult.AttractionCategoryId.ToString().Should().Be("513fb459-ab65-4858-98ab-6424806be77e");
            attractionResult.Category.Name.Should().Be("Temple");
            attractionResult.IsFree.Should().Be(true);
            attractionResult.CityId.ToString().Should().Be("21d106af-0880-4eae-aa98-e8a5957a29c6");
            attractionResult.City.Name.Should().Be("Rome");

            _mockAttractionRepository.Verify(repo => repo.GetByIdWithRelatedDataAsync(It.IsAny<Guid>()), Times.Once());
        }

        [Fact]
        public void Handle_InvalidCommand_ShouldThrowValidationException()
        {
            var handler = new GetAttractionDetailQueryHandler(_mapper, _mockAttractionRepository.Object);

            Func<Task> func = async () => await handler.Handle(new GetAttractionDetailQuery(), CancellationToken.None);

            func.Should().Throw<ValidationException>().Where(e => e.Errors.Count == 1 && e.Errors.Any(x => x.Contains("Id cannot be empty")));

            _mockAttractionRepository.Verify(repo => repo.GetByIdWithRelatedDataAsync(It.IsAny<Guid>()), Times.Never());
        }
    }
}
