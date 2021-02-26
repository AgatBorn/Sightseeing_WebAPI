using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sightseeing.Application.Features.Countries.Commands.CreateCountry;
using Sightseeing.Application.Features.Countries.Queries.GetAllCountries;
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

        [HttpPost]
        public async Task<ActionResult<Application.Features.Countries.Commands.CreateCountry.CountryDto>> CreateCountry([FromBody] CreateCountryCommand createCountryCommand)
        {
            var countryDto = await _mediator.Send(createCountryCommand);

            return Ok(countryDto);
        }

        [HttpGet]
        public async Task<ActionResult<CountriesListVm>> GetAllCities()
        {
            var countriesListVm = await _mediator.Send(new GetAllCountriesQuery());

            return Ok(countriesListVm);
        }
    }
}
