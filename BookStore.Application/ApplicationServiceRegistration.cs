using BookStore.Application.Features.Genre.Commands;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Application;

public static class ApplicationServiceRegistration
{
     public static void AddApplicationServices(this IServiceCollection services)
     {
          services.AddMediatR(o =>
          {
               o.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
          });
          services.AddTransient<IValidator<AddGenreCommand>, AddGenreCommandValidator>();
          services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
     }
}