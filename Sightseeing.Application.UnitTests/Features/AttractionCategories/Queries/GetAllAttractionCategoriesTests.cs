using AutoMapper;
using FluentAssertions;
using Moq;
using Sightseeing.Application.Contracts.Persistence;
using Sightseeing.Application.Features.AttractionCategories.Queries.GetAllCategories;
using Sightseeing.Application.Profiles;
using Sightseeing.Application.UnitTests.Mocks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sightseeing.Application.UnitTests.Features.AttractionCategories.Queries
{
    public class GetAllAttractionCategoriesTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAttractionCategoryRepository> _mockAttractionCategoryRepository;

        public GetAllAttractionCategoriesTests()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();

            _mockAttractionCategoryRepository = RepositoryMocks.GetAttractionCategoryRepository();
        }

        [Fact]
        public async Task Handle_ValidQuery_ShouldReturnList()
        {
            var listCount = (await _mockAttractionCategoryRepository.Object.GetAllAsync()).Count;

            var handler = new GetAllAttractionCategoriesQueryHandler(_mapper, _mockAttractionCategoryRepository.Object);

            var result = await handler.Handle(new GetAllAttractionCategoriesQuery(), CancellationToken.None);

            result.Should().BeOfType(typeof(AttractionCategoriesListVm));
            result.Categories.Should().BeOfType(typeof(List<AttractionCategoryDto>));
            result.Categories.Count.Should().Be(listCount);
            result.Count.Should().Be(listCount);

            _mockAttractionCategoryRepository.Verify(repo => repo.GetAllAsync(), Times.Exactly(2)); // first in this test
        }
    }
}
