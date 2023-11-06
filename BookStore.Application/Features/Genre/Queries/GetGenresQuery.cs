using BookStore.Application.Contracts.Genre;
using BookStore.Application.Features.Book;
using MediatR;

namespace BookStore.Application.Features.Genre.Queries
{
     public record GetGenresQuery : IRequest<IEnumerable<GenreDto>?>;
     public class GetGenresQueryHandler :IRequestHandler<GetGenresQuery,IEnumerable<GenreDto>?>
     {
          private readonly IGetGenres _selectAll;

          public GetGenresQueryHandler(IGetGenres selectAll)
          {
               _selectAll = selectAll;
          }

          public async Task<IEnumerable<GenreDto>?> Handle(GetGenresQuery request, CancellationToken cancellationToken)
          {
               var genres = await _selectAll.GetAsync();
               return genres.Select(genre => new GenreDto
               {
                    Id = genre.Id,
                    Name = genre.Name,
                    Description = genre.Description,
                    Books = genre.Books!.Select(book => new BookDto
                    {
                         Id = book.Id,
                         Title = book.Title,
                         Description = book.Description,
                         Price = book.Price,
                         Quantity = book.Quantity,
                         AuthorId = book.AuthorId,
                    })
               });
          }
     }
}
