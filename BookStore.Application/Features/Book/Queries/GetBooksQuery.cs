using BookStore.Application.Contracts.Book;
using BookStore.Application.Exceptions;
using BookStore.Application.Features.Author;
using BookStore.Application.Features.Genre;
using MediatR;

namespace BookStore.Application.Features.Book.Queries
{
     public record GetBooksQuery : IRequest<IEnumerable<BookDto>?>;
     public class GetBooksQueryHandler :IRequestHandler<GetBooksQuery, IEnumerable<BookDto>?>
     {
          private readonly IGetBooks _getBooks;

          public GetBooksQueryHandler(IGetBooks getBooks)
          {
               _getBooks = getBooks;
          }

          public async Task<IEnumerable<BookDto>?> Handle(GetBooksQuery request, CancellationToken cancellationToken)
          {
               var books = await _getBooks.GetAsync();
               var enumerable = books.ToList();
               if (enumerable.Any())
               {
                    var result = enumerable.Select(book => new BookDto
                    {
                         Id = book.Id,
                         Title = book.Title,
                         Description = book.Description,
                         Price = book.Price,
                         Quantity = book.Quantity,
                         AuthorId = book.AuthorId,
                         GenreId = book.GenreId,
                         Author = new AuthorDto
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
                         
                    }).ToList();
                    return result;
               }

               throw new NotFoundException();
          }
     }
}
