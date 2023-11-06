using BookStore.Application.Contracts.User;
using BookStore.Application.Exceptions;
using BookStore.Application.Features.Role;
using FluentValidation;
using MediatR;

namespace BookStore.Application.Features.User.Commands.SetRole
{
     public record SetRoleCommand(SetRoleRequest Model) :IRequest<UserDto?>
     {
          public Guid RoleId = Model.RoleId;
          public string? UserEmail = Model.UserEmail;
     }
     public class SetRoleCommandHandler :IRequestHandler<SetRoleCommand,UserDto?>
     {
          private readonly  IValidator<SetRoleCommand> _validator;
          private readonly ISetUserRole _setUserRole;

          public SetRoleCommandHandler(IValidator<SetRoleCommand> validator, ISetUserRole setUserRole)
          {
               _validator = validator;
               _setUserRole = setUserRole;
          }

          public async Task<UserDto?> Handle(SetRoleCommand request, CancellationToken cancellationToken)
          {
               var validation = await _validator.ValidateAsync(request, cancellationToken);
               if (!validation.IsValid)
               {
                    throw new ValidationException(validation.Errors);
               }

               var user = await _setUserRole.SetAsync(request.UserEmail, request.RoleId);
               if (user is { Role: { } })
               {
                    var result = new UserDto
                    {
                         Id = user.Id,
                         Email = user.Email,
                         RoleId = user.RoleId,
                         Role = new RoleDto
                         {
                              Id = user.Role.Id,
                              Name = user.Role.Name,
                         }
                    };
                    return result;
               }

               throw new DataBaseErrorException();
          }
     }
}
