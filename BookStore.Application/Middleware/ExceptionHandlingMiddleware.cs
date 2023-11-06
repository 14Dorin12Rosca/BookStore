using System.Net;
using BookStore.Application.Errors;
using BookStore.Application.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BookStore.Application.Middleware
{
     public class ExceptionHandlingMiddleware :IMiddleware
     {
          private readonly ILogger<ExceptionHandlingMiddleware> _logger;

          public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
          {
               _logger = logger;
          }

          public async Task InvokeAsync(HttpContext context, RequestDelegate next)
          {
               try
               {
                    await next(context);
               }
               catch (KeyNotFoundException)
               {
                    await HandleExceptionAsync(context,
                         HttpStatusCode.NotFound, "NotFound");
               }
               catch (ValidationException ex)
               {
                    await HandleExceptionAsync(context,
                         HttpStatusCode.BadRequest, "BadRequest",
                         ex.Errors);
               }
               catch (NotFoundException)
               {
                    await HandleExceptionAsync(context,
                         HttpStatusCode.NotFound, "NotFound");
               }
               catch (DataBaseErrorException)
               {
                    await HandleExceptionAsync(context,
                         HttpStatusCode.ServiceUnavailable, "DataBaseError");
               }
          }
               private async Task HandleExceptionAsync(HttpContext context,
               HttpStatusCode code,
               string message,
               IEnumerable<ValidationFailure>? errors = null)
          {
               HttpResponse response = context.Response;
               response.ContentType = "application/json";
               response.StatusCode = (int)code;
               ErrorDto error = new()
               {
                    Message = message,
                    StatusCode = (int)code,
                    ValidationErrors = errors
               };
               string result = JsonSerializer.Serialize(error);
               await response.WriteAsync(result);
          }

     }
}
