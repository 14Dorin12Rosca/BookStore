using BookStore.Application.Contracts.DataBase;
using BookStore.Application.Contracts.User;
using FluentValidation;

namespace BookStore.Application.Features.User.Commands.SetRole
{
     public class SetRoleCommandValidator :AbstractValidator<SetRoleCommand>
     {
          private readonly IGetEntityById<Domain.Entities.Role> _getRole;
          private readonly IGetUserByEmail _getUser;
          public SetRoleCommandValidator(IGetEntityById<Domain.Entities.Role> getRole, IGetUserByEmail getUser)
          {
               _getRole = getRole;
               _getUser = getUser;
               RuleFor(x => x.RoleId)

                    .NotNull().WithMessage("The role id is required")
                    .NotEmpty().WithMessage("The role id cannot be empty")
                    .MustAsync((x, _) => RoleExist(x)).WithMessage("role with such id does not exist");
               RuleFor(x => x.UserEmail)
                    .NotNull().WithMessage("The user email is required")
                    .NotEmpty().WithMessage("The user email cannot be empty")
                    .EmailAddress().WithMessage("Invalid email format")
                    .MustAsync((x, _) => UserExist(x)).WithMessage("User with such email does not exist");

          }

          private async Task<bool> RoleExist(Guid roleId)
          {
               var role = await _getRole.GetAsync(roleId);
               return role != null;
          }
          private async Task<bool> UserExist(string userEmail)
          {
               var user = await _getUser.GetAsync(userEmail);
               return user != null;
          }
     }
}
