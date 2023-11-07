using BookStore.Application.Contracts.DataBase;
using BookStore.Application.Contracts.User;
using FluentValidation;

namespace BookStore.Application.Features.Book.Commands.Buy
{
     public class BuyBookCommandValidator :AbstractValidator<BuyBookCommand>
     {
          private readonly IGetEntityById<Domain.Entities.Book> _getBook;
          private readonly IGetUserByEmail _getUser;
          public BuyBookCommandValidator(IGetEntityById<Domain.Entities.Book> getBook, IGetUserByEmail getUser)
          {
               _getBook = getBook;
               _getUser = getUser;
               RuleFor(x => x.BookId)

                    .NotNull().WithMessage("The book id is required")
                    .NotEmpty().WithMessage("The book id cannot be empty")
                    .MustAsync((x, _) => BookExist(x)).WithMessage("book with such id does not exist");
               RuleFor(x => x.UserEmail)
                    .NotNull().WithMessage("The user email is required")
                    .NotEmpty().WithMessage("The user email cannot be empty")
                    .EmailAddress().WithMessage("Invalid email format")
                    .MustAsync((x, _) => UserExist(x)).WithMessage("User with such email does not exist");

          }

          private async Task<bool> BookExist(Guid roleId)
          {
               var book = await _getBook.GetAsync(roleId);
               return book != null;
          }
          private async Task<bool> UserExist(string userEmail)
          {
               var user = await _getUser.GetAsync(userEmail);
               return user != null;
          }
     }
}
