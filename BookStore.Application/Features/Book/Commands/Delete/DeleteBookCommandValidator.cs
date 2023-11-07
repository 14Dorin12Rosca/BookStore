using FluentValidation;

namespace BookStore.Application.Features.Book.Commands.Delete
{
    internal class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
             RuleFor(g => g.Id)
                  .NotNull().WithMessage("The id is required")
                  .NotEmpty().WithMessage("The id cannot be empty");
             //check if it exist

        }
    }
}
