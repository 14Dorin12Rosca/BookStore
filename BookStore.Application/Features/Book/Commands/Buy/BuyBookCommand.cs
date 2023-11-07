using BookStore.Application.Contracts.Book;
using BookStore.Application.Contracts.DataBase;
using BookStore.Application.Exceptions;
using FluentValidation;
using MediatR;

namespace BookStore.Application.Features.Book.Commands.Buy
{
     public record BuyBookCommand(Guid BookId, string UserEmail) :IRequest<bool>
     {
          public Guid BookId = BookId;
          public string UserEmail = UserEmail;
     }
     public class BuyBookCommandHandler:IRequestHandler<BuyBookCommand,bool>
     {
          private readonly IBuyBook _buyBook;
          private readonly IGetEntityById<Domain.Entities.Book> _getBook;
          private readonly IUpdateEntity<Domain.Entities.Book> _updateBook;
          private readonly IValidator<BuyBookCommand> _validator;

          public BuyBookCommandHandler(IBuyBook buyBook, IGetEntityById<Domain.Entities.Book> getBook, IUpdateEntity<Domain.Entities.Book> updateBook, IValidator<BuyBookCommand> validator)
          {
               _buyBook = buyBook;
               _getBook = getBook;
               _updateBook = updateBook;
               _validator = validator;
          }

          public async Task<bool> Handle(BuyBookCommand request, CancellationToken cancellationToken)
          {
               var validation = await _validator.ValidateAsync(request, cancellationToken);
               if (!validation.IsValid)
               {
                    throw new ValidationException(validation.Errors);
               }
               var book = await _getBook.GetAsync(request.BookId);
               if (book?.Quantity <= 0)
               {
                    throw new NotFoundException("there are no books in stock");
               }
               var result = await _buyBook.BuyAsync(request.BookId, request.UserEmail);
               if (!result) throw new DataBaseErrorException();
               var updated = book != null && await GetBookQuantityAsync(book);
               if (updated)
               {
                    return true;
               }
               throw new DataBaseErrorException();
          }

          private async Task<bool> GetBookQuantityAsync(Domain.Entities.Book book)
          {
               book.Quantity = book.Quantity-1;
               var result = await _updateBook.UpdateAsync(book);
               if (result)
               {
                    return true;
               }

               throw new DataBaseErrorException();
          }
     }
}
