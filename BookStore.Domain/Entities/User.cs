namespace BookStore.Domain.Entities
{
     public class User
     {
          public Guid Id { get; set; }
          public string? Email { get; set; }
          public string? Password { get; set; }
          public Guid? RoleId { get; set; }
          public Role? Role { get; set; }
          public IEnumerable<Book>? Books { get; set; }
     }
}
