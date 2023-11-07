using FluentValidation;

namespace BookStore.Application.Features.Book.Queries.GetBook
{
     public class GetBookQueryValidator :AbstractValidator<GetBookQuery>
     {
          public GetBookQueryValidator()
          {
               RuleFor(b => b.Id)
                    .NotNull().WithMessage("The id is required")
                    .NotEmpty().WithMessage("The id cannot be empty");

          }
     }
}
