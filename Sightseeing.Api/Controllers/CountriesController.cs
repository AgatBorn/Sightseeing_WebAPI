using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sightseeing.Application.Features.Countries.Commands;
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
        public async Task<ActionResult<CountryDto>> CreateCountry([FromBody] CreateCountryCommand createCountryCommand)
        {
            var countryDto = await _mediator.Send(createCountryCommand);

            return Ok(countryDto);
        }
    }
}
