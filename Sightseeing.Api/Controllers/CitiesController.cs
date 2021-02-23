using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sightseeing.Application.Features.Cities.Commands.CreateCity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sightseeing.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        public async Task<ActionResult<CityDto>> CreateCity([FromBody] CreateCityCommand createCityCommand)
        {
            var cityDto = await _mediator.Send(createCityCommand);

            return Ok(cityDto);
            //return CreatedAtAction(nameof(GetAttractionDetails), new { id = response.Attraction.AttractionId }, response);
        }
    }
}
