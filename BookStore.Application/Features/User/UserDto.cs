using BookStore.Application.Features.Role;

namespace BookStore.Application.Features.User
{
     public class UserDto
     {
          public Guid Id { get; set; }
          public string? Email { get; set; }
          public Guid? RoleId { get; set; }
          public RoleDto? Role { get; set; }

     }
}
