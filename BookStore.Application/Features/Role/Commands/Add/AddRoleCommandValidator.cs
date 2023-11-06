using FluentValidation;

namespace BookStore.Application.Features.Role.Commands.Add
{
    internal class AddRoleCommandValidator : AbstractValidator<AddRoleCommand>
    {
        public AddRoleCommandValidator()
        {
            RuleFor(g => g.Name)
                 .NotNull().WithMessage("The Role name is required")
                 .NotEmpty().WithMessage("The Role name cannot be empty")
                 .MaximumLength(64).WithMessage("The role maximum length is 64 characters");
        }
    }
}
