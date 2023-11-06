using BookStore.Application.Contracts.User;
using FluentValidation;

namespace BookStore.Application.Features.Login.Commands.Login
{
    internal class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        private readonly IGetUserByEmail _getUser;
        public LoginCommandValidator(IGetUserByEmail getUser)
        {
            _getUser = getUser;
            RuleFor(x => x.Email)
                 .NotNull().WithMessage("The email is required")
                 .NotEmpty().WithMessage("The email cannot be empty")
                 .EmailAddress().WithMessage("Invalid email format")
                 .MustAsync((x, _) => ExistAsync(x)).WithMessage("User with such email does not exist");
            RuleFor(x => x.Password)
                 .NotNull().WithMessage("The password is required")
                 .NotEmpty().WithMessage("The password cannot be empty")
                 .MinimumLength(8).WithMessage("The password length cannot be less than 8 characters");

            RuleFor(x => x).MustAsync((x, _) => PasswordIsCorrect(x)).WithMessage("The Password is wrong");

        }

        private async Task<bool> ExistAsync(string email)
        {
            var user = await _getUser.GetAsync(email);
            return user != null;
        }
        private async Task<bool> PasswordIsCorrect(LoginCommand model)
        {
             if (model.Email == null) return false;
             var user = await _getUser.GetAsync(model.Email);
             return user != null && BCrypt.Net.BCrypt.Verify(model.Model.Password, user.Password);
        }
    }
}
