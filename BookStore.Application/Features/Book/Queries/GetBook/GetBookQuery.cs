using BookStore.Application.Contracts.Book;
using BookStore.Application.Exceptions;
using BookStore.Application.Features.Author;
using BookStore.Application.Features.Genre;
using FluentValidation;
using MediatR;

namespace BookStore.Application.Features.Book.Queries.GetBook
{
    public record GetBookQuery(Guid Id) : IRequest<BookDto?>
    {
        public Guid Id = Id;
    }
    public class GetBookQueryHandler:IRequestHandler<GetBookQuery,BookDto?>
    {
         private readonly IValidator<GetBookQuery> _validator;
         private readonly IGetBook _getBook;

         public GetBookQueryHandler(IValidator<GetBookQuery> validator, IGetBook getBook)
         {
              _validator = validator;
              _getBook = getBook;
         }

         public async Task<BookDto?> Handle(GetBookQuery request, CancellationToken cancellationToken)
         {
              var validation = await _validator.ValidateAsync(request, cancellationToken);
              if (!validation.IsValid)
              {
                   throw new ValidationException(validation.Errors);
              }

              var book = await _getBook.GetAsync(request.Id);
              if (book == null)
              {
                   throw new NotFoundException();
              }
              var result = new BookDto
              {
                   Id = book.Id,
                   Title = book.Title,
                   Description = book.Description,
                   Price = book.Price,
                   Quantity = book.Quantity,
                   AuthorId = book.AuthorId,
                   GenreId = book.GenreId,
                   Author  =new AuthorDto
                   {
                        Id = book.Author!.Id,
                        FirstName = book.Author.FirstName,
                        LastName = book.Author.LastName
                   },
                   Genre = new GenreDto
                   {
                        Id = book.Genre!.Id,
                        Name = book.Genre.Name,
                        Description = book.Genre.Description,
                   },
              };
              return result;
         }
     }
}
