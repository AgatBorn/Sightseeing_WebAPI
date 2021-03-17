using FluentAssertions;
using Sightseeing.Api.IntegrationTests.Common;
using Sightseeing.Application.Features.Attractions.Commands.CreateAttraction;
using Sightseeing.Application.Features.Attractions.Queries.GetAttractionDetail;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sightseeing.Api.IntegrationTests.Controllers
{
    public class AttractionsControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public AttractionsControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAttractionDetails_ShouldReturnSuccessWithData()
        {
            var client = _factory.GetClient();

            string guid = "80f20e38-6a81-4670-ad7a-b98ea83d2aa1";

            var response = await client.GetAsync($"/attractions/{guid}");

            response.EnsureSuccessStatusCode();

            var result = await Utilities.GetResponseContent<AttractionDetailVm>(response);

            result.Should().BeOfType(typeof(AttractionDetailVm));
            result.Should().NotBeNull();
            result.AttractionId.ToString().Should().Be(guid);
        }

        [Fact]
        public async Task CreateAttraction_ShouldReturnSuccessWithCreatedData()
        {
            var client = _factory.GetClient();

            var token = ApiTokenHelper.GenerateFakeToken();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var command = new CreateAttractionCommand
            {
                Name = "Pantheon",
                Date = "113–125 AD",
                Author = "Apollodoros",
                Description = "The building is cylindrical with a portico of large granite Corinthian columns (eight in the first rank and two groups of four behind) under a pediment.",
                AttractionCategoryId = Guid.Parse("{7e12db9e-5648-4385-a9dc-abe24f1fbc4b}"),
                IsFree = true,
                CityId = Guid.Parse("{b3ec95dd-c424-4a2b-a329-50e6bdfff8b8}"),
            };

            var content = Utilities.GetRequestContent(command);
            var response = await client.PostAsync($"/attractions", content);

            response.EnsureSuccessStatusCode();

            var result = await Utilities.GetResponseContent<AttractionDto>(response);

            result.Should().BeOfType(typeof(AttractionDto));
            result.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.LocalPath.Should().Be($"/Attractions/{result.AttractionId}");
        }

        [Fact]
        public async Task CreateAttraction_ShouldReturnUnauthorized()
        {
            var client = _factory.GetClient();

            var attractionCommand = new CreateAttractionCommand
            {
                Name = "Pantheon",
                Date = "113–125 AD",
                Author = "Apollodoros",
                Description = "The building is cylindrical with a portico of large granite Corinthian columns (eight in the first rank and two groups of four behind) under a pediment.",
                AttractionCategoryId = Guid.Parse("{7e12db9e-5648-4385-a9dc-abe24f1fbc4b}"),
                IsFree = true,
                CityId = Guid.Parse("{b3ec95dd-c424-4a2b-a329-50e6bdfff8b8}"),
            };

            var content = Utilities.GetRequestContent(attractionCommand);
            var response = await client.PostAsync($"/attractions", content);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
