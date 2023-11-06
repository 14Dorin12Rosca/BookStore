using BookStore.Application.Features.Genre;
using BookStore.Application.Features.User;
using BookStore.Domain.Entities;

namespace BookStore.Application.Features.Book
{
     public class BookDto
     {
          public Guid? Id { get; set; }
          public string? Title { get; set; }
          public string? Description { get; set; }
          public double? Price { get; set; }
          public int? Quantity { get; set; }
          public Guid? AuthorId { get; set; }
          public Guid? GenreId { get; set; }

          public Author? Author { get; set; }
          public GenreDto? Genre { get; set; }
          public ICollection<UserDto>? Users { get; set; }
     }
}
