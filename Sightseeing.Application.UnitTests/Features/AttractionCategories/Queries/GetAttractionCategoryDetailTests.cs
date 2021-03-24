using AutoMapper;
using FluentAssertions;
using Moq;
using Sightseeing.Application.Contracts.Persistence;
using Sightseeing.Application.Exceptions;
using Sightseeing.Application.Features.AttractionCategories.Queries.GetAttractionCategoryDetail;
using Sightseeing.Application.Profiles;
using Sightseeing.Application.UnitTests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sightseeing.Application.UnitTests.Features.AttractionCategories.Queries
{
    public class GetAttractionCategoryDetailTests
    {
        private IMapper _mapper;
        private Mock<IAttractionCategoryRepository> _mockAttractionCategoryRepository;

        public GetAttractionCategoryDetailTests()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();

            _mockAttractionCategoryRepository = RepositoryMocks.GetAttractionCategoryRepository();
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldGetTheDetails()
        {
            var handler = new GetAttractionCategoryDetailQueryHandler(_mapper, _mockAttractionCategoryRepository.Object);

            var command = new GetAttractionCategoryDetailQuery { Id = Guid.Parse("{3ae4e108-e2df-4893-958a-2d76ab89b9dc}") };

            var attractionResult = await handler.Handle(command, CancellationToken.None);

            attractionResult.Should().BeOfType(typeof(AttractionCategoryDetailVm));
            attractionResult.Name.Should().Be("Garden");

            _mockAttractionCategoryRepository.Verify(repo => repo.GetByIdWithRelatedDataAsync(It.IsAny<Guid>()), Times.Once());
        }

        [Fact]
        public void Handle_InvalidCommand_ShouldThrowValidationException()
        {
            var handler = new GetAttractionCategoryDetailQueryHandler(_mapper, _mockAttractionCategoryRepository.Object);

            Func<Task> func = async () => await handler.Handle(new GetAttractionCategoryDetailQuery(), CancellationToken.None);

            func.Should().Throw<ValidationException>().Where(e => e.Errors.Count == 1 && e.Errors.Any(x => x.Contains("Id cannot be empty")));

            _mockAttractionCategoryRepository.Verify(repo => repo.GetByIdWithRelatedDataAsync(It.IsAny<Guid>()), Times.Never());
        }
    }
}
