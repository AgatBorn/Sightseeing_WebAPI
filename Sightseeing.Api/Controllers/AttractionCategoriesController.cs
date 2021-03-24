using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sightseeing.Application.Features.AttractionCategories.Commands.CreateAttractionCategory;
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

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<AttractionCategoryDto>> CreateCity
            ([FromBody] CreateAttractionCategoryCommand createAttractionCategoryCommand)
        {
            var response = await _mediator.Send(createAttractionCategoryCommand);

            return CreatedAtAction(nameof(GetAttractionCategoryDetails), new { id = response.Id }, response);
        }

        [HttpGet]
        public async Task<ActionResult<List<AttractionCategoryListVm>>> GetAllAttractionCategories()
        {
            var list = await _mediator.Send(new GetAllAttractionCategoriesQuery());

            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AttractionCategoryDetailVm>> GetAttractionCategoryDetails(Guid id)
        {
            var attractionCategoryVm = await _mediator.Send(new GetAttractionCategoryDetailQuery() { Id = id });

            return Ok(attractionCategoryVm);
        }
    }
}
