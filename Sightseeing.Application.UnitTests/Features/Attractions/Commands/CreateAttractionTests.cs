using AutoMapper;
using FluentAssertions;
using Moq;
using Sightseeing.Application.Contracts.Persistence;
using Sightseeing.Application.Exceptions;
using Sightseeing.Application.Features.Attractions.Commands.CreateAttraction;
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

namespace Sightseeing.Application.UnitTests.Features.Attractions.Commands
{
    public class CreateAttractionTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAttractionRepository> _mockAttractionRepository;

        public CreateAttractionTests()
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
            var initialListCount = (await _mockAttractionRepository.Object.GetAllAsync()).Count;

            var mockAttractionCategoryRepository = RepositoryMocks.GetAttractionCategoryRepository();
            mockAttractionCategoryRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new AttractionCategory());

            var mockCityRepository = RepositoryMocks.GetCityRepository();
            mockCityRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new City());

            var handler = new CreateAttractionCommandHandler(_mapper, _mockAttractionRepository.Object, mockAttractionCategoryRepository.Object, mockCityRepository.Object);

            var command = new CreateAttractionCommand
            {
                Name = "Colosseum",
                Date = "70–80 AD",
                Author = "Vespasian",
                Description = "It is an oval amphitheatre in the centre of the city of Rome, Italy, just east of the Roman Forum and is the largest ancient amphitheatre ever built, and is still the largest standing amphitheater in the world today, despite its age.",
                AttractionCategoryId = Guid.Parse("{dcaf14b8-3a1e-4b77-9957-8e38c0025e9b}"),
                IsFree = true,
                CityId = Guid.Parse("{21d106af-0880-4eae-aa98-e8a5957a29c6}")
            };

            var attractionResult = await handler.Handle(command, CancellationToken.None);

            attractionResult.Should().BeOfType(typeof(AttractionDto));
            attractionResult.Name.Should().Be("Colosseum");
            attractionResult.Date.Should().Be("70–80 AD");
            attractionResult.Author.Should().Be("Vespasian");
            attractionResult.Description.Should().Be("It is an oval amphitheatre in the centre of the city of Rome, Italy, just east of the Roman Forum and is the largest ancient amphitheatre ever built, and is still the largest standing amphitheater in the world today, despite its age.");
            attractionResult.AttractionCategoryId.ToString().Should().Be("dcaf14b8-3a1e-4b77-9957-8e38c0025e9b");
            attractionResult.IsFree.Should().Be(true);
            attractionResult.CityId.ToString().Should().Be("21d106af-0880-4eae-aa98-e8a5957a29c6");

            var categories = await _mockAttractionRepository.Object.GetAllAsync();

            categories.Count.Should().Be(initialListCount + 1);

            _mockAttractionRepository.Verify(repo => repo.AddAsync(It.IsAny<Attraction>()), Times.Once());
        }

        [Fact]
        public void Handle_InvalidCommand_ShouldThrowValidationException()
        {
            var mockAttractionCategoryRepository = RepositoryMocks.GetAttractionCategoryRepository();
            mockAttractionCategoryRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((AttractionCategory)null);

            var mockCityRepository = RepositoryMocks.GetCityRepository();
            mockCityRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((City)null);

            var handler = new CreateAttractionCommandHandler(_mapper, _mockAttractionRepository.Object, mockAttractionCategoryRepository.Object, mockCityRepository.Object);

            Func<Task> func = async () => await handler.Handle(new CreateAttractionCommand(), CancellationToken.None);

            func.Should().Throw<ValidationException>().Where(e => e.Errors.Count == 6 
                && e.Errors.Any(x => x.Contains("Name cannot be empty"))
                && e.Errors.Any(x => x.Contains("Attraction Category Id cannot be empty"))
                && e.Errors.Any(x => x.Contains("Cannot find attraction category with that id"))
                && e.Errors.Any(x => x.Contains("City Id cannot be empty"))
                && e.Errors.Any(x => x.Contains("Cannot find city with that id"))
                && e.Errors.Any(x => x.Contains("Price is required")));

            _mockAttractionRepository.Verify(repo => repo.AddAsync(It.IsAny<Attraction>()), Times.Never());
        }
    }
}
