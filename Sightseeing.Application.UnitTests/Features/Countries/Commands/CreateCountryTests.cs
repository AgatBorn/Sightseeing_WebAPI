using AutoMapper;
using FluentAssertions;
using Moq;
using Sightseeing.Application.Exceptions;
using Sightseeing.Application.Features.Countries.Commands.CreateCountry;
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

namespace Sightseeing.Application.UnitTests.Features.Countries.Commands
{
    public class CreateCountryTests
    {
        private readonly IMapper _mapper;

        public CreateCountryTests()
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
            var mockCountryRepository = RepositoryMocks.GetCountryRepository();
            mockCountryRepository.Setup(repo => repo.IsCountryNameUnique(It.IsAny<string>())).ReturnsAsync(true);

            var initialListCount = (await mockCountryRepository.Object.GetAllAsync()).Count;

            var handler = new CreateCountryCommandHandler(_mapper, mockCountryRepository.Object);

            var result = await handler.Handle(new CreateCountryCommand { Name = "Naples" }, CancellationToken.None);

            result.Should().BeOfType(typeof(CountryDto));
            result.Name.Should().Be("Naples");

            var countries = await mockCountryRepository.Object.GetAllAsync();

            countries.Count.Should().Be(initialListCount + 1);

            mockCountryRepository.Verify(repo => repo.AddAsync(It.IsAny<Country>()), Times.Once());
        }

        [Fact]
        public void Handle_InvalidCommand_ShouldThrowValidationException()
        {
            var mockCountryRepository = RepositoryMocks.GetCountryRepository();
            mockCountryRepository.Setup(repo => repo.IsCountryNameUnique(It.IsAny<string>())).ReturnsAsync(false);

            var handler = new CreateCountryCommandHandler(_mapper, mockCountryRepository.Object);

            Func<Task> func = async () => await handler.Handle(new CreateCountryCommand { Name = "" }, CancellationToken.None);

            func.Should().Throw<ValidationException>().Where(e => e.Errors.Count == 2
            && e.Errors.Any(x => x.Contains("Name cannot be empty"))
            && e.Errors.Any(x => x.Contains("Country with that name already exists")));

            mockCountryRepository.Verify(repo => repo.AddAsync(It.IsAny<Country>()), Times.Never());
        }
    }
}
