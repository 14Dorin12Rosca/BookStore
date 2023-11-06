using BookStore.Application.Features.Role;
using BookStore.Application.Features.Role.Commands.Add;
using BookStore.Application.Features.Role.Queries;
using BookStore.Application.Features.Role.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookStore.Api.Role
{
     [Route("api/[controller]")]
     [ApiController]
     public class RoleController : ControllerBase
     {

          private readonly IMediator _mediator;

          public RoleController(IMediator mediator)
          {
               _mediator = mediator;
          }

          /// <summary>
          /// creates a new role
          /// </summary>
          /// <param name="request">the request to create a role</param>
          /// <returns>the created role</returns>
          [SwaggerResponse(StatusCodes.Status200OK, "The created Role", typeof(RoleDto))]
          [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
          [HttpPost]
          public async Task<IActionResult> Create([FromBody] CreateRoleRequest request)
          {
               var cmd = new AddRoleCommand(request);
               var result = await _mediator.Send(cmd);
               return Ok(result);
          }

          /// <summary>
          /// get a role by id
          /// </summary>
          /// <param name="id">the id of requested role</param>
          /// <returns>the role data if it exist</returns>
          [Authorize(Roles = "Admin")]
          [SwaggerResponse(StatusCodes.Status200OK, "The Role data", typeof(RoleDto))]
          [SwaggerResponse(StatusCodes.Status404NotFound, "Requested Role Not Found")]
          [SwaggerResponse(StatusCodes.Status500InternalServerError, "Database Error")]
          [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized request")]
          [HttpGet("{id}")]
          public async Task<IActionResult> Get(Guid id)
          {
               var query = new GetRoleQuery(id);
               var result = await _mediator.Send(query);
               return Ok(result);
          }
          /// <summary>
          /// get a list of roles.
          /// </summary>
          /// <returns>the role data if it exist</returns>
          [Authorize(Roles = "Admin")]
          [SwaggerResponse(StatusCodes.Status200OK, "The Role data", typeof(IEnumerable<RoleDto>))]
          [SwaggerResponse(StatusCodes.Status404NotFound, "Requested Roles Not Found")]
          [SwaggerResponse(StatusCodes.Status500InternalServerError, "Database Error")]
          [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized request")]
          [HttpGet]
          public async Task<IActionResult> Get()
          {
               var query = new GetRolesQuery();
               var result = await _mediator.Send(query);
               return Ok(result);
          }
     }
}
