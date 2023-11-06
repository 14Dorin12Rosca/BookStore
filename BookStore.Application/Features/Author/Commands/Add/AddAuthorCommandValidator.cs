using FluentValidation;

namespace BookStore.Application.Features.Author.Commands.Add
{
     public class AddAuthorCommandValidator :AbstractValidator<AddAuthorCommand>
     {
          public AddAuthorCommandValidator()
          {
               RuleFor(a => a.FirstName)
                    .NotNull().WithMessage("The author first name is required")
                    .NotEmpty().WithMessage("The author first name cannot be empty");


               RuleFor(a => a.LastName)
                    .NotNull().WithMessage("The author last name is required")
                    .NotEmpty().WithMessage("The author last name cannot be empty");
               //check if author already exist
          }
     }
}
