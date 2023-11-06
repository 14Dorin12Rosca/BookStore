using BookStore.Application.Features.Genre;
using BookStore.Application.Features.Genre.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
          [HttpPost]
          public async Task<IActionResult> Create([FromBody] CreateGenreRequest request)
          {
               var cmd = new AddGenreCommand(request);
               var result = await _mediator.Send(cmd);
               return Ok(result);
          }
     }
}
