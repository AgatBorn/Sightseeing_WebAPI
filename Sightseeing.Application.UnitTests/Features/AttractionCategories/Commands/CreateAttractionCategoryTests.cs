using AutoMapper;
using FluentAssertions;
using Moq;
using Sightseeing.Application.Contracts.Persistence;
using Sightseeing.Application.Exceptions;
using Sightseeing.Application.Features.AttractionCategories.Commands.CreateAttractionCategory;
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

namespace Sightseeing.Application.UnitTests.Features.AttractionCategories.Commands
{
    public class CreateAttractionCategoryTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAttractionCategoryRepository> _mockAttractionCategoryRepository;

        public CreateAttractionCategoryTests()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();

            _mockAttractionCategoryRepository = RepositoryMocks.GetAttractionCategoryRepository();
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldAddToRepository()
        {
            var initialListCount = (await _mockAttractionCategoryRepository.Object.GetAllAsync()).Count;

            var handler = new CreateAttractionCategoryCommandHandler(_mapper, _mockAttractionCategoryRepository.Object);

            var result = await handler.Handle(new CreateAttractionCategoryCommand { Name = "Church" }, CancellationToken.None);

            result.Should().BeOfType(typeof(AttractionCategoryDto));
            result.Name.Should().Be("Church");

            var categories = await _mockAttractionCategoryRepository.Object.GetAllAsync();

            categories.Count.Should().Be(initialListCount + 1);

            _mockAttractionCategoryRepository.Verify(repo => repo.AddAsync(It.IsAny<AttractionCategory>()), Times.Once());
        }

        [Fact]
        public void Handle_InvalidCommand_EmptyName_ShouldThrowValidationException()
        {
            var handler = new CreateAttractionCategoryCommandHandler(_mapper, _mockAttractionCategoryRepository.Object);

            Func<Task> func = async () => await handler.Handle(new CreateAttractionCategoryCommand { Name = "" }, CancellationToken.None);

            func.Should().Throw<ValidationException>();

            _mockAttractionCategoryRepository.Verify(repo => repo.AddAsync(It.IsAny<AttractionCategory>()), Times.Never());
        }

        [Fact]
        public void Handle_InvalidCommand_TooLongName_ShouldThrowValidationException()
        {
            var handler = new CreateAttractionCategoryCommandHandler(_mapper, _mockAttractionCategoryRepository.Object);
            var command = new CreateAttractionCategoryCommand { Name = "ThisNameShouldHaveMoreThan50CharactersSoINeedToAddAFewMore" };

            Func<Task> func = async () => await handler.Handle(command, CancellationToken.None);
            
            func.Should().Throw<ValidationException>().Where(e => e.Errors.Any(x => x.Contains("must not exceed 50 characters")));

            _mockAttractionCategoryRepository.Verify(repo => repo.AddAsync(It.IsAny<AttractionCategory>()), Times.Never());
        }
    }
}
