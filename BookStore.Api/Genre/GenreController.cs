using BookStore.Application.Features.Genre;
using BookStore.Application.Features.Genre.Commands.Add;
using BookStore.Application.Features.Genre.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
          /// creates a new genre
          /// </summary>
          /// <param name="request">the request to create a genre</param>
          /// <returns>the created genre</returns>
          [SwaggerResponse(StatusCodes.Status200OK, "The created genre", typeof(GenreDto))]
          [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
          [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized action")]
          [SwaggerResponse(StatusCodes.Status500InternalServerError, "Database Error")]
          [HttpPost]
          [Authorize(Roles = "Admin")]
          public async Task<IActionResult> Create([FromBody] CreateGenreRequest request)
          {
               var cmd = new AddGenreCommand(request);
               var result = await _mediator.Send(cmd);
               return Ok(result);
          }
          /// <summary>
          /// get list of genres.
          /// </summary>
          /// <returns> list of genres with their books</returns>
          [SwaggerResponse(StatusCodes.Status200OK, "The list of genres was returned", typeof(IEnumerable<GenreDto>))]
          [SwaggerResponse(StatusCodes.Status500InternalServerError, "Database Error")]
          [HttpGet]
          public async Task<IActionResult> Get()
          {
               var cmd = new GetGenresQuery();
               var result = await _mediator.Send(cmd);
               return Ok(result);
          }
     }
}
