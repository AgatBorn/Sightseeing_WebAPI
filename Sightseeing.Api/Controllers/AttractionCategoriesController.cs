using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sightseeing.Application.Features.AttractionCategories.Queries.GetAllCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sightseeing.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AttractionCategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AttractionCategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        public async Task<ActionResult<Application.Features.AttractionCategories.Commands.AttractionCategoryDto>> CreateCity
            ([FromBody] Application.Features.AttractionCategories.Commands.CreateAttractionCategoryCommand createAttractionCategoryCommand)
        {
            var response = await _mediator.Send(createAttractionCategoryCommand);

            return Ok(response);
            //return CreatedAtAction(nameof(GetAttractionDetails), new { id = response.Attraction.AttractionId }, response);
        }

        [HttpGet()]
        public async Task<ActionResult<List<AttractionCategoriesListVm>>> GetAllAttractionCategories()
        {
            var attractionCategoriesListVm = await _mediator.Send(new GetAllAttractionCategoriesQuery());

            return Ok(attractionCategoriesListVm);
        }
    }
}
