using BookStore.Application.Features.Book;

namespace BookStore.Application.Features.Author
{
     public class AuthorDto
     {
          public Guid? Id { get; set; }
          public string? FirstName { get; set; }
          public string? LastName { get; set; }
          public IEnumerable<BookDto>? Books { get; set; }
     }
}
