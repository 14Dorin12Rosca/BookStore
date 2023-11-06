using BookStore.Application.Features.Genre;
using BookStore.Application.Features.Genre.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookStore.Api.Genre
{
     [Route("api/[controller]")]
     [ApiController]
     public class GenreController : ControllerBase
     {

          private readonly IMediator _mediator;

          public GenreController(IMediator mediator)
          {
               _mediator = mediator;
          }

          /// <summary>
          /// Creates a whole new genre.
          /// </summary>
          /// <param name="request">The request to create a genre.</param>
          /// <returns>The created genre.</returns>
          [HttpPost]
          [SwaggerResponse(StatusCodes.Status200OK, "The created genre", typeof(GenreDto))]
          [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
          [HttpPost]
          public async Task<IActionResult> Create([FromBody] CreateGenreRequest request)
          {
               var cmd = new AddGenreCommand(request);
               var result = await _mediator.Send(cmd);
               return Ok(result);
          }
     }
}
