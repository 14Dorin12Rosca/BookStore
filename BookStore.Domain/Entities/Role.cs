namespace BookStore.Domain.Entities
{
     public class Role :AuditableEntity
     {
          public Guid Id { get; set; }
          public string? Name { get; set; }

          public ICollection<User>? Users { get; set; }
     }
}
