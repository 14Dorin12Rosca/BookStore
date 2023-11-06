using BookStore.Application.Contracts.Author;
using BookStore.Application.Features.Book;
using MediatR;

namespace BookStore.Application.Features.Author.Queries
{
     public record GetAuthorsQuery : IRequest<IEnumerable<AuthorDto>>;
     public class GetAuthorsQueryHandler :IRequestHandler<GetAuthorsQuery,IEnumerable<AuthorDto>>
     {
          private readonly IGetAuthors _selectAll;

          public GetAuthorsQueryHandler(IGetAuthors selectAll)
          {
               _selectAll = selectAll;
          }

          public async Task<IEnumerable<AuthorDto>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
          {
               var authors = await _selectAll.GetAsync();
               return authors.Select(author => new AuthorDto()
               {
                    Id = author.Id,
                    FirstName = author.FirstName,
                    LastName = author.LastName,
                    Books = author.Books!.Select(book => new BookDto
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
