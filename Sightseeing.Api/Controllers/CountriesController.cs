using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sightseeing.Application.Features.Countries.Commands.CreateCountry;
using Sightseeing.Application.Features.Countries.Queries.GetAllCountries;
using Sightseeing.Application.Features.Countries.Queries.GetCountryDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sightseeing.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CountriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Application.Features.Countries.Commands.CreateCountry.CountryDto>> CreateCountry([FromBody] CreateCountryCommand createCountryCommand)
        {
            var countryDto = await _mediator.Send(createCountryCommand);

            return CreatedAtAction(nameof(GetCountryDetails), new { id = countryDto.CountryId }, countryDto);
        }

        [HttpGet]
        public async Task<ActionResult<CountriesListVm>> GetAllCountries()
        {
            var countriesListVm = await _mediator.Send(new GetAllCountriesQuery());

            return Ok(countriesListVm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDetailVm>> GetCountryDetails(Guid id)
        {
            var countryVm = await _mediator.Send(new GetCountryDetailQuery() { Id = id });

            return Ok(countryVm);
        }
    }
}
