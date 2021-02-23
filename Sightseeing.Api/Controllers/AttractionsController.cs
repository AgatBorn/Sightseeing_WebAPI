using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sightseeing.Application.Features.Attractions.Commands.CreateAttraction;
using Sightseeing.Application.Features.Attractions.Queries.GetAttractionDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sightseeing.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AttractionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AttractionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AttractionDetailVm>> GetAttractionDetails(Guid id)
        {
            var attraction = await _mediator.Send(new GetAttractionDetailQuery() { Id = id });

            return Ok(attraction);
        }

        [HttpPost()]
        public async Task<ActionResult<AttractionDto>> CreateAttraction([FromBody] CreateAttractionCommand createAttractionCommand)
        {
            var attractionDto = await _mediator.Send(createAttractionCommand);

            return CreatedAtAction(nameof(GetAttractionDetails), new { id = attractionDto.AttractionId }, attractionDto);
        }
    }
}
