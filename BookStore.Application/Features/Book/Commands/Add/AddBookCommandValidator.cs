using FluentValidation;

namespace BookStore.Application.Features.Book.Commands.Add
{
    internal class AddBookCommandValidator : AbstractValidator<AddBookCommand>
    {
        public AddBookCommandValidator()
        {
             RuleFor(g => g.Description)
                  .NotNull().WithMessage("The description is required")
                  .NotEmpty().WithMessage("The description cannot be empty");
             RuleFor(b => b.Title)
                  .NotNull().WithMessage("The title is required")
                  .NotEmpty().WithMessage("The title cannot be empty");
             RuleFor(b => b.Price)
                  .NotNull().WithMessage("The price is required")
                  .NotEmpty().WithMessage("The price cannot be empty")
                  .GreaterThan(0);
             RuleFor(b => b.Quantity)
                  .NotNull().WithMessage("The quantity is required")
                  .NotEmpty().WithMessage("The quantity cannot be empty")
                  .GreaterThan(0);
             RuleFor(b => b.AuthorId)
                  .NotNull().WithMessage("The author id is required")
                  .NotEmpty().WithMessage("The author id cannot be empty");
             RuleFor(b => b.GenreId)
                  .NotNull().WithMessage("The genre Id is required")
                  .NotEmpty().WithMessage("The genre Id cannot be empty");
            //check if book already exist
     }
    }
}
