using BookStore.Application.Features.Author;
using BookStore.Application.Features.Author.Commands.Add;
using BookStore.Application.Features.Author.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookStore.Api.Author
{
     [Route("api/[controller]")]
     [ApiController]
     public class AuthorController : ControllerBase
     {
          private readonly IMediator _mediator;

          public AuthorController(IMediator mediator)
          {
               _mediator = mediator;
          }

          /// <summary>
          /// add a new author
          /// </summary>
          /// <param name="request">the request to add a new author</param>
          /// <returns>the created author</returns>
          [SwaggerResponse(StatusCodes.Status200OK, "The created author", typeof(AuthorDto))]
          [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
          [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized action")]
          [SwaggerResponse(StatusCodes.Status500InternalServerError, "Database Error")]
          [HttpPost]
          [Authorize(Roles = "Admin")]
          public async Task<IActionResult> Create([FromBody] AddAuthorRequest request)
          {
               var cmd = new AddAuthorCommand(request);
               var result = await _mediator.Send(cmd);
               return Ok(result);
          }
          /// <summary>
          /// get list of authors.
          /// </summary>
          /// <returns> list of authors with their books</returns>
          [SwaggerResponse(StatusCodes.Status200OK, "The list of authors was returned", typeof(IEnumerable<AuthorDto>))]
          [SwaggerResponse(StatusCodes.Status500InternalServerError, "Database Error")]
          [HttpGet]
          public async Task<IActionResult> Get()
          {
               var cmd = new GetAuthorsQuery();
               var result = await _mediator.Send(cmd);
               return Ok(result);
          }
     }
}
