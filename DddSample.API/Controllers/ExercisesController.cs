using DddSample.Application.Common;
using DddSample.Application.Exercises.CreateExercise;
using DddSample.Application.Exercises.Dto;
using DddSample.Application.Exercises.GetExercises;
using DddSample.Application.Exercises.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DddSample.API.Controllers
{
    [ApiController]
    [Route("api/exercises")]
    public sealed class ExercisesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ExercisesController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<ExerciseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResult<ExerciseDto>>> Get(
            [FromQuery] string? search,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 50,
            [FromQuery] ExerciseSortBy sortBy = ExerciseSortBy.Name,
            [FromQuery] bool desc = false,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(
                new GetExercisesQuery(search, page, pageSize, sortBy, desc),
                cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ExerciseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ExerciseDto>> Create(
            [FromBody] CreateExerciseCommand cmd,
            CancellationToken cancellationToken)
        {
            var dto = _mediator.Send(cmd, cancellationToken);

            return Created($"/api/exercises/{dto.Id}", dto);

            //REST Convention for fronend where show api address where do you get a id
            //return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }
    }
}
