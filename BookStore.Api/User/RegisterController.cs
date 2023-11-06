using BookStore.Application.Features.Role;
using BookStore.Application.Features.User;
using BookStore.Application.Features.User.Commands.AddUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookStore.Api.User
{
     [Route("api/[controller]")]
     [ApiController]
     public class RegisterController : ControllerBase
     {
          private readonly IMediator _mediator;

          public RegisterController(IMediator mediator)
          {
               _mediator = mediator;
          }

          /// <summary>
          /// Register a new user.
          /// </summary>
          /// <param name="request">The request to create a user.</param>
          /// <returns>The created user data</returns>
          [SwaggerResponse(StatusCodes.Status200OK, "The user is created", typeof(RoleDto))]
          [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
          [HttpPost]
          public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
          {
               var cmd = new AddUserCommand(request);
               var result = await _mediator.Send(cmd);
               return Ok(result);
          }

     }
}
