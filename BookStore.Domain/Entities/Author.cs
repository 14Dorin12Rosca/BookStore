namespace BookStore.Domain.Entities
{
     public class Author :AuditableEntity
     {
          public Guid? Id { get; set; }
          public string? FirstName { get; set; }
          public string? LastName { get; set; }
          public IEnumerable<Book>? Books { get; set; }
     }
}
