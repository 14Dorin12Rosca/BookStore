namespace BookStore.Application.Features.Login
{
     public record LoginRequest
     {
          public string? Email { get; set; }
          public string? Password { get; set; }
     }

}
