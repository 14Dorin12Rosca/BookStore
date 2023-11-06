using BookStore.Application.Features.User;

namespace BookStore.Application.Features.Role
{
     public class RoleDto
     {
          public Guid Id { get; set; }
          public string? Name { get; set; }
          public IEnumerable<UserDto>? Users { get; set; }
     }
}
