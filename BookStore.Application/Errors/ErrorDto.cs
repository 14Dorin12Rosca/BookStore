using System.Text.Json;
using FluentValidation.Results;

namespace BookStore.Application.Errors
{
     public class ErrorDto
     {
          public int StatusCode { get; set; }

          public string Message { get; set; }

          public IEnumerable<ValidationFailure>? ValidationErrors { get; set; }
          public override string ToString()
          {
               return JsonSerializer.Serialize(this);
          }
     }
}
