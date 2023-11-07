using BookStore.Application.Contracts.Book;
using BookStore.Application.Contracts.DataBase;
using BookStore.Application.Exceptions;
using FluentValidation;
using MediatR;

namespace BookStore.Application.Features.Book.Commands.Supply
{
     public record SupplyBookCommand(BookSupplyRequest Model) :IRequest<BookDto?>
     {
          public Guid BookId =Model.BookId;
          public int Quantity =Model.Quantity;
     }
     public class SupplyBookCommandHandler:IRequestHandler<SupplyBookCommand,BookDto?>
     {
          private readonly IValidator<SupplyBookCommand> _validator;
          private readonly IGetBook _getBook;
          private readonly IUpdateEntity<Domain.Entities.Book> _updateBook;
          public SupplyBookCommandHandler(IValidator<SupplyBookCommand> validator, IGetBook getBook, IUpdateEntity<Domain.Entities.Book> updateBook)
          {
               _validator = validator;
               _getBook = getBook;
               _updateBook = updateBook;
          }

          public async Task<BookDto?> Handle(SupplyBookCommand request, CancellationToken cancellationToken)
          {
               var validation = await _validator.ValidateAsync(request, cancellationToken);
               if (!validation.IsValid)
               {
                    throw new ValidationException(validation.Errors);
               }

               var book = await _getBook.GetAsync(request.BookId);
               if (book == null)
               {
                    throw new NotFoundException();
               }
               book.Quantity += request.Quantity;
               var result = await _updateBook.UpdateAsync(book);
               //to change
               if (result)
                    return new BookDto()
                    {
                         Id = Guid.NewGuid(),
                         Title = book.Title,
                         Description = book.Description,
                         Price = book.Price,
                         Quantity = book.Quantity,
                         AuthorId = book.AuthorId,
                         GenreId = book.GenreId
                    };
               throw new DataBaseErrorException();
          }
     }
}
