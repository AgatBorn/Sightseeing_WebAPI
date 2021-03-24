using FluentAssertions;
using Sightseeing.Api.IntegrationTests.Common;
using Sightseeing.Application.Features.Cities.Commands.CreateCity;
using Sightseeing.Application.Features.Cities.Queries.GetAllCities;
using Sightseeing.Application.Features.Cities.Queries.GetCityDetail;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sightseeing.Api.IntegrationTests.Controllers
{
    public class CitiesControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public CitiesControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetCityDetails_ShouldReturnSuccessWithData()
        {
            var client = _factory.GetClient();

            string guid = "54210db9-2f35-482e-9254-90c38e5aa684";

            var response = await client.GetAsync($"/cities/{guid}");

            response.EnsureSuccessStatusCode();

            var result = await Utilities.GetResponseContent<CityDetailVm>(response);

            result.Should().BeOfType(typeof(CityDetailVm));
            result.Should().NotBeNull();
            result.Id.ToString().Should().Be(guid);
        }

        [Fact]
        public async Task GetAllCities_ShouldReturnSuccessWithData()
        {
            var client = _factory.GetClient();

            var response = await client.GetAsync($"/cities");

            response.EnsureSuccessStatusCode();

            var result = await Utilities.GetResponseContent<List<CityListVm>>(response);

            result.Should().BeOfType(typeof(List<CityListVm>));
            result.Should().NotBeNull();
            result.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task CreateCity_ShouldReturnSuccessWithCreatedData()
        {
            var client = _factory.GetClient();

            var token = ApiTokenHelper.GenerateFakeToken();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var command = new CreateCityCommand
            {
                Name = "Venice",
                CountryId = Guid.Parse("{3ae4e108-e2df-4893-958a-2d76ab89b9dc}")
            };

            var content = Utilities.GetRequestContent(command);
            var response = await client.PostAsync($"/cities", content);

            response.EnsureSuccessStatusCode();

            var result = await Utilities.GetResponseContent<Application.Features.Cities.Commands.CreateCity.CityDto>(response);

            result.Should().BeOfType(typeof(Application.Features.Cities.Commands.CreateCity.CityDto));
            result.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.LocalPath.Should().Be($"/Cities/{result.CityId}");
        }

        [Fact]
        public async Task CreateCity_ShouldReturnUnauthorized()
        {
            var client = _factory.GetClient();

            var command = new CreateCityCommand
            {
                Name = "Venice",
                CountryId = Guid.Parse("{3ae4e108-e2df-4893-958a-2d76ab89b9dc}")
            };

            var content = Utilities.GetRequestContent(command);
            var response = await client.PostAsync($"/cities", content);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
