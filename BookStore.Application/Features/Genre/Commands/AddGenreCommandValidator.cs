using FluentValidation;

namespace BookStore.Application.Features.Genre.Commands
{
     internal class AddGenreCommandValidator :AbstractValidator<AddGenreCommand>
     {
          public AddGenreCommandValidator()
          {
               RuleFor(g => g.Name)
                    .NotNull().WithMessage("The genre name is required")
                    .NotEmpty().WithMessage("The genre name cannot be empty")
                    .MaximumLength(64);


               RuleFor(g => g.Description)
                    .NotNull().WithMessage("The genre name is required")
                    .NotEmpty().WithMessage("The genre name cannot be empty")
                    .MaximumLength(64);
          }
     }
}
