using FluentValidation;

namespace BookStore.Application.Features.Book.Commands.Supply
{
    internal class SupplyBookCommandValidator : AbstractValidator<SupplyBookCommand>
    {
        public SupplyBookCommandValidator()
        {
             RuleFor(b => b.Quantity)
                  .NotNull().WithMessage("The quantity is required")
                  .NotEmpty().WithMessage("The quantity cannot be empty")
                  .GreaterThan(0);
             RuleFor(b => b.BookId)
                  .NotNull().WithMessage("The book id is required")
                  .NotEmpty().WithMessage("The book id cannot be empty");
             
            //check if book already exist
     }
    }
}
