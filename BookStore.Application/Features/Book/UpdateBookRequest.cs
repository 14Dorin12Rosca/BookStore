namespace BookStore.Application.Features.Book
{
     public class UpdateBookRequest
     {
          public Guid Id { get; set; }
          public string? Title { get; set; }
          public string? Description { get; set; }
          public double? Price { get; set; }
          public int? Quantity { get; set; }
          public Guid? AuthorId { get; set; }
          public Guid? GenreId { get; set; }
     }
}
