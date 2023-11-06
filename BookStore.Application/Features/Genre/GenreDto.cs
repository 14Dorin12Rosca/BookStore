using BookStore.Application.Features.Book;

namespace BookStore.Application.Features.Genre
{
     public class GenreDto
     {
          public Guid Id { get; set; }
          public string? Name { get; set; }
          public string? Description { get; set; }
          public IEnumerable<BookDto>? Books { get; set; }
     }
}
