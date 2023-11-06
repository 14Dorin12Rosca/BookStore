namespace BookStore.Application.Features.User
{
     public class SetRoleRequest
     {
          public Guid RoleId { get; set; }
          public string? UserEmail { get; set; }
     }
}
