using System.Security.Authentication;
using BookStore.Application.Features.Book;
using BookStore.Application.Features.Book.Commands.Add;
using BookStore.Application.Features.Book.Commands.Delete;
using BookStore.Application.Features.Book.Commands.Supply;
using BookStore.Application.Features.Book.Commands.Update;
using BookStore.Application.Features.Book.Queries;
using BookStore.Application.Features.Book.Queries.GetBook;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using BookStore.Application.Features.Book.Commands.Buy;

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
          public async Task<IActionResult> GetById( Guid id)
          {
               var cmd = new GetBookQuery(id);
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
          [HttpPut]
          [Authorize(Roles = "Admin")]
          public async Task<IActionResult> Update([FromBody] UpdateBookRequest request)
          {
               var cmd = new UpdateBookCommand(request);
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

          /// <summary>
          /// supply the reserves of books
          /// </summary>
          /// <param name="request">the request that have id of supplied book and quantity</param>
          /// <returns>the result of operation</returns>
          [SwaggerResponse(StatusCodes.Status200OK, "The result of operation", typeof(BookDto))]
          [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
          [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized action")]
          [SwaggerResponse(StatusCodes.Status500InternalServerError, "Database Error")]
          [HttpPatch("supply")]
          [Authorize(Roles = "Admin")]
          public async Task<IActionResult> Supply(BookSupplyRequest request)
          {
               var cmd = new SupplyBookCommand(request);
               var result = await _mediator.Send(cmd);
               return Ok(result);
          }

          /// <summary>
          /// buy a book
          /// </summary>
          /// <param name="id">the the id of book</param>
          /// <returns>the result of operation</returns>
          [SwaggerResponse(StatusCodes.Status200OK, "The result of operation", typeof(bool))]
          [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
          [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized action")]
          [SwaggerResponse(StatusCodes.Status500InternalServerError, "Database Error")]
          [HttpPost("id")]
          [Authorize(Roles = "Admin")]
          public async Task<IActionResult> Buy(Guid id)
          {
               var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
               if (userEmail != null)
               {
                    var cmd = new BuyBookCommand(id,userEmail);
                    var result = await _mediator.Send(cmd);
                    return Ok(result);
               }

               throw new AuthenticationException();
          }
     }
}
