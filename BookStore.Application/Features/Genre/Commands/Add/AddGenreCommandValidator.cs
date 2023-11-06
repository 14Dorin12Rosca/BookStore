using FluentValidation;

namespace BookStore.Application.Features.Genre.Commands.Add
{
    internal class AddGenreCommandValidator : AbstractValidator<AddGenreCommand>
    {
        public AddGenreCommandValidator()
        {
            RuleFor(g => g.Name)
                 .NotNull().WithMessage("The genre name is required")
                 .NotEmpty().WithMessage("The genre name cannot be empty")
                 .MaximumLength(64).WithMessage("The genre name maximum length is 64 characters");


            RuleFor(g => g.Description)
                 .NotNull().WithMessage("The genre name is required")
                 .NotEmpty().WithMessage("The genre name cannot be empty");
        }
    }
}
