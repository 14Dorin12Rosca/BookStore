using BookStore.Application.Features.Book;
using BookStore.Application.Features.Book.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookStore.Api.Book
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
          /// get list of books.
          /// </summary>
          /// <returns> list of books with their author and genre</returns>
          [SwaggerResponse(StatusCodes.Status200OK, "The list of books was returned", typeof(IEnumerable<BookDto>))]
          [SwaggerResponse(StatusCodes.Status500InternalServerError, "Database Error")]
          [HttpGet]
          public async Task<IActionResult> Get()
          {
               var cmd = new GetBooksQuery();
               var result = await _mediator.Send(cmd);
               return Ok(result);
          }

          /// <summary>
          /// get book details by id.
          /// </summary>
          /// <returns> a book data with author and genre</returns>
          [SwaggerResponse(StatusCodes.Status200OK, "The book was returned", typeof(IEnumerable<BookDto>))]
          [SwaggerResponse(StatusCodes.Status500InternalServerError, "Database Error")]
          [HttpGet("{id}")]
          public async Task<IActionResult> GetById()
          {
               var cmd = new GetBooksQuery();
               var result = await _mediator.Send(cmd);
               return Ok(result);
          }
     }
}
