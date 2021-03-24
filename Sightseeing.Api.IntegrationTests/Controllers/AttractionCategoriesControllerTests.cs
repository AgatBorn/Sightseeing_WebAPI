using FluentAssertions;
using Sightseeing.Api.IntegrationTests.Common;
using Sightseeing.Application.Features.AttractionCategories.Commands.CreateAttractionCategory;
using Sightseeing.Application.Features.AttractionCategories.Queries.GetAllCategories;
using Sightseeing.Application.Features.AttractionCategories.Queries.GetAttractionCategoryDetail;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sightseeing.Api.IntegrationTests.Controllers
{
    public class AttractionCategoriesControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public AttractionCategoriesControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAttractionCategoryDetails_ShouldReturnSuccessWithData()
        {
            var client = _factory.GetClient();

            string guid = "24a31ba7-4030-48ed-a705-c5584f7ff860";

            var response = await client.GetAsync($"/attractioncategories/{guid}");

            response.EnsureSuccessStatusCode();

            var result = await Utilities.GetResponseContent<AttractionCategoryDetailVm>(response);

            result.Should().BeOfType(typeof(AttractionCategoryDetailVm));
            result.Should().NotBeNull();
            result.Id.ToString().Should().Be(guid);
        }

        [Fact]
        public async Task GetAllAttractionCategories_ShouldReturnSuccessWithData()
        {
            var client = _factory.GetClient();

            var response = await client.GetAsync($"/attractioncategories");

            response.EnsureSuccessStatusCode();

            var result = await Utilities.GetResponseContent<List<AttractionCategoryListVm>>(response);

            result.Should().BeOfType(typeof(List<AttractionCategoryListVm>));
            result.Should().NotBeNull();
            result.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task CreateAttractionCategory_ShouldReturnSuccessWithCreatedData()
        {
            var client = _factory.GetClient();

            var token = ApiTokenHelper.GenerateFakeToken();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var command = new CreateAttractionCategoryCommand
            {
                Name = "Fountain"
            };

            var content = Utilities.GetRequestContent(command);
            var response = await client.PostAsync($"/attractioncategories", content);

            response.EnsureSuccessStatusCode();

            var result = await Utilities.GetResponseContent<Application.Features.AttractionCategories.Commands.CreateAttractionCategory.AttractionCategoryDto>(response);

            result.Should().BeOfType(typeof(Application.Features.AttractionCategories.Commands.CreateAttractionCategory.AttractionCategoryDto));
            result.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.LocalPath.Should().Be($"/AttractionCategories/{result.Id}");
        }

        [Fact]
        public async Task CreateAttractionCategory_ShouldReturnUnauthorized()
        {
            var client = _factory.GetClient();

            var command = new CreateAttractionCategoryCommand
            {
                Name = "Fountain"
            };

            var content = Utilities.GetRequestContent(command);
            var response = await client.PostAsync($"/attractioncategories", content);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
