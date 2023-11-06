using BookStore.Application.Features.Login;
using BookStore.Application.Features.Login.Commands.Login;
using BookStore.Application.Features.User;
using BookStore.Application.Features.User.Commands.SetRole;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookStore.Api.User
{
     [Route("api/[controller]")]
     [ApiController]
     public class UserController : ControllerBase
     {
          private readonly IMediator _mediator;

          public UserController(IMediator mediator)
          {
               _mediator = mediator;
          }
          /// <summary>
          /// Login into System.
          /// </summary>
          /// <param name="request">The request to login.</param>
          /// <returns>The Jwt token of user</returns>
          [SwaggerResponse(StatusCodes.Status200OK, "The token was generated", typeof(string))]
          [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
          [SwaggerResponse(StatusCodes.Status500InternalServerError, "Database Error")]
          [HttpPost]
          public async Task<IActionResult> Login([FromBody] LoginRequest request)
          {
               var cmd = new LoginCommand(request);
               var result = await _mediator.Send(cmd);
               return Ok(result);
          }

          /// <summary>
          /// Set User Role.
          /// </summary>
          /// <param name="request">The request to pass user email and role id.</param>
          /// <returns>The user data</returns>
          [SwaggerResponse(StatusCodes.Status200OK, "role was attributed to user", typeof(UserDto))]
          [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
          [SwaggerResponse(StatusCodes.Status500InternalServerError, "Database Error")]
          [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized request")]
          [Authorize(Roles = "Admin")]
          [HttpPatch("add/role")]
          public async Task<IActionResult> SetRole([FromBody] SetRoleRequest request)
          {
               var cmd = new SetRoleCommand(request);
               var result = await _mediator.Send(cmd);
               if (result != null)
               {
                    return Ok(result);

               }
               return Problem();
          }
     }
}
