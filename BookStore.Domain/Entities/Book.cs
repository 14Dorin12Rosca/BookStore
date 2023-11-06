namespace BookStore.Domain.Entities
{
     public class Book :AuditableEntity
     {
          public Guid? Id { get; set; }
          public string? Title { get; set; }
          public string? Description { get; set; }
          public double? Price { get; set; }
          public int? Quantity { get; set; }
          public Guid? AuthorId { get; set; }
          public Guid? GenreId { get; set; }

          public Author? Author { get; set; }
          public Genre? Genre { get; set; }
          public IEnumerable<User>? Users { get; set; }
     }
}
