using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sightseeing.Application.Features.AttractionCategories.Queries.GetAllCategories;
using Sightseeing.Application.Features.AttractionCategories.Queries.GetAttractionCategoryDetail;
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

        [HttpPost]
        public async Task<ActionResult<Application.Features.AttractionCategories.Commands.CreateAttractionCategory.AttractionCategoryDto>> CreateCity
            ([FromBody] Application.Features.AttractionCategories.Commands.CreateAttractionCategory.CreateAttractionCategoryCommand createAttractionCategoryCommand)
        {
            var response = await _mediator.Send(createAttractionCategoryCommand);

            return CreatedAtAction(nameof(GetAttractionCategoryDetails), new { id = response.Id }, response);
        }

        [HttpGet]
        public async Task<ActionResult<AttractionCategoriesListVm>> GetAllAttractionCategories()
        {
            var attractionCategoriesListVm = await _mediator.Send(new GetAllAttractionCategoriesQuery());

            return Ok(attractionCategoriesListVm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AttractionCategoryVm>> GetAttractionCategoryDetails(Guid id)
        {
            var attractionCategoryVm = await _mediator.Send(new GetAttractionCategoryDetailQuery() { Id = id });

            return Ok(attractionCategoryVm);
        }
    }
}
