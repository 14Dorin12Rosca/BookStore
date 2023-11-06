using BookStore.Application.Contracts.User;
using FluentValidation;

namespace BookStore.Application.Features.User.Commands.AddUser
{
     internal class AddUserCommandValidator :AbstractValidator<AddUserCommand>
     {
          private readonly IGetUserByEmail _getUser;
          public AddUserCommandValidator(IGetUserByEmail getUser)
          {
               _getUser = getUser;
               RuleFor(x => x.Email)
                    .NotNull().WithMessage("The email is required")
                    .NotEmpty().WithMessage("The email cannot be empty")
                    .EmailAddress().WithMessage("Invalid email format")
                    .MustAsync((x, _) => NotExistAsync(x)).WithMessage("User with such email already exist");

               RuleFor(x => x.Password)
                    .NotNull().WithMessage("The password is required")
                    .NotEmpty().WithMessage("The password cannot be empty")
                    .MinimumLength(8).WithMessage("The password must be at least 8 characters long");

               RuleFor(x => x.ConfirmPassword)
                    .NotNull().WithMessage("The confirm password is required")
                    .NotEmpty().WithMessage("The confirm password cannot be empty")
                    .Equal(x => x.Password).WithMessage("The confirm password does not match the password");
          }

          private async Task<bool> NotExistAsync(string email)
          {
               var user =await  _getUser.GetAsync(email);
               return user == null;
          }
     }
}
