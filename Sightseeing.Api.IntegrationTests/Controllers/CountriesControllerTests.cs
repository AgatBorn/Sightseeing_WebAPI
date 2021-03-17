using FluentAssertions;
using Sightseeing.Api.IntegrationTests.Common;
using Sightseeing.Application.Features.Countries.Commands.CreateCountry;
using Sightseeing.Application.Features.Countries.Queries.GetAllCountries;
using Sightseeing.Application.Features.Countries.Queries.GetCountryDetail;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sightseeing.Api.IntegrationTests.Controllers
{
    public class CountriesControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public CountriesControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetCountryDetails_ShouldReturnSuccessWithData()
        {
            var client = _factory.GetClient();

            string guid = "3ae4e108-e2df-4893-958a-2d76ab89b9dc";

            var response = await client.GetAsync($"/countries/{guid}");

            response.EnsureSuccessStatusCode();

            var result = await Utilities.GetResponseContent<CountryDetailVm>(response);

            result.Should().BeOfType(typeof(CountryDetailVm));
            result.Should().NotBeNull();
            result.Id.ToString().Should().Be(guid);
        }

        [Fact]
        public async Task GetAllCountries_ShouldReturnSuccessWithData()
        {
            var client = _factory.GetClient();

            var response = await client.GetAsync($"/countries");

            response.EnsureSuccessStatusCode();

            var result = await Utilities.GetResponseContent<CountriesListVm>(response);

            result.Should().BeOfType(typeof(CountriesListVm));
            result.Should().NotBeNull();
            result.Count.Should().Be(3);
        }

        [Fact]
        public async Task CreateCountry_ShouldReturnSuccessWithCreatedData()
        {
            var client = _factory.GetClient();

            var token = ApiTokenHelper.GenerateFakeToken();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var command = new CreateCountryCommand
            {
                Name = "England"
            };

            var content = Utilities.GetRequestContent(command);
            var response = await client.PostAsync($"/countries", content);

            response.EnsureSuccessStatusCode();

            var result = await Utilities.GetResponseContent<Application.Features.Countries.Commands.CreateCountry.CountryDto>(response);

            result.Should().BeOfType(typeof(Application.Features.Countries.Commands.CreateCountry.CountryDto));
            result.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.LocalPath.Should().Be($"/Countries/{result.CountryId}");
        }

        [Fact]
        public async Task CreateCountry_ShouldReturnUnauthorized()
        {
            var client = _factory.GetClient();

            var command = new CreateCountryCommand
            {
                Name = "England"
            };

            var content = Utilities.GetRequestContent(command);
            var response = await client.PostAsync($"/countries", content);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
