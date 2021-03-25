using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sightseeing.Application.Features.Attractions.Commands.CreateAttraction;
using Sightseeing.Application.Features.Attractions.Commands.DeleteAttraction;
using Sightseeing.Application.Features.Attractions.Commands.UpdateAttraction;
using Sightseeing.Application.Features.Attractions.Queries.GetAllAtractions;
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

        [HttpGet]
        public async Task<ActionResult<List<AttractionListVm>>> GetAllAttractions()
        {
            var list = await _mediator.Send(new GetAllAttractionsQuery());

            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AttractionDetailVm>> GetAttractionDetails(Guid id)
        {
            var attraction = await _mediator.Send(new GetAttractionDetailQuery() { Id = id });

            return Ok(attraction);
        }

        [Authorize]
        [HttpPost()]
        public async Task<ActionResult<AttractionDto>> CreateAttraction([FromBody] CreateAttractionCommand createAttractionCommand)
        {
            var attractionDto = await _mediator.Send(createAttractionCommand);

            return CreatedAtAction(nameof(GetAttractionDetails), new { id = attractionDto.AttractionId }, attractionDto);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<UpdatedAttractionDto>> UpdateAttraction([FromBody] UpdateAttractionCommand updateAttractionCommand)
        {
            var attractionDto = await _mediator.Send(updateAttractionCommand);

            return Ok(attractionDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAttraction(Guid id)
        {
            await _mediator.Send(new DeleteAttractionCommand() { Id = id });

            return NoContent();
        }
    }
}
