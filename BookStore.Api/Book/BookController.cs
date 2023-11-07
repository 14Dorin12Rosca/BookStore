using BookStore.Application.Features.Book;
using BookStore.Application.Features.Book.Commands.Add;
using BookStore.Application.Features.Book.Commands.Delete;
using BookStore.Application.Features.Book.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookStore.Api.Book
{
     [Route("api/[controller]")]
     [ApiController]
     public class BookController : ControllerBase
     {
          private readonly IMediator _mediator;

          public BookController(IMediator mediator)
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

          /// <summary>
          /// creates a new book
          /// </summary>
          /// <param name="request">the request to create a book</param>
          /// <returns>the created book</returns>
          [SwaggerResponse(StatusCodes.Status200OK, "The created book", typeof(BookDto))]
          [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
          [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized action")]
          [SwaggerResponse(StatusCodes.Status500InternalServerError, "Database Error")]
          [HttpPost]
          [Authorize(Roles = "Admin")]
          public async Task<IActionResult> Create([FromBody] CreateBookRequest request)
          {
               var cmd = new AddBookCommand(request);
               var result = await _mediator.Send(cmd);
               return Ok(result);
          }

          /// <summary>
          /// updates a existing book
          /// </summary>
          /// <param name="request">the request to update a book</param>
          /// <returns>the updated book</returns>
          [SwaggerResponse(StatusCodes.Status200OK, "The updated book", typeof(BookDto))]
          [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
          [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized action")]
          [SwaggerResponse(StatusCodes.Status500InternalServerError, "Database Error")]
          [HttpPatch]
          [Authorize(Roles = "Admin")]
          public async Task<IActionResult> Update([FromBody] UpdateBookRequest request)
          {
               var cmd = new UpdateBookRequest();
               var result = await _mediator.Send(cmd);
               return Ok(result);
          }

          /// <summary>
          /// delete a existing book
          /// </summary>
          /// <param name="id">the id of deleted book</param>
          /// <returns>the result of operation</returns>
          [SwaggerResponse(StatusCodes.Status200OK, "The result of operation", typeof(bool))]
          [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
          [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized action")]
          [SwaggerResponse(StatusCodes.Status500InternalServerError, "Database Error")]
          [HttpDelete]
          [Authorize(Roles = "Admin")]
          public async Task<IActionResult> Delete(Guid id)
          {
               var cmd = new DeleteBookCommand(id);
               var result = await _mediator.Send(cmd);
               return Ok(result);
          }
     }
}
