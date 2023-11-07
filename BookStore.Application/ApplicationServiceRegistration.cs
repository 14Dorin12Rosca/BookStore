using BookStore.Application.Features.Author.Commands.Add;
using BookStore.Application.Features.Book.Queries.GetBook;
using BookStore.Application.Features.Genre.Commands.Add;
using BookStore.Application.Features.Login.Commands.Login;
using BookStore.Application.Features.Role.Commands.Add;
using BookStore.Application.Features.Role.Queries.Get;
using BookStore.Application.Features.User.Commands.AddUser;
using BookStore.Application.Features.User.Commands.SetRole;
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
          //Class Validations
          services.AddTransient<IValidator<AddGenreCommand>, AddGenreCommandValidator>();
          services.AddTransient<IValidator<AddRoleCommand>, AddRoleCommandValidator>();
          services.AddTransient<IValidator<GetRoleQuery>, GetRoleQueryValidator>();
          services.AddTransient<IValidator<AddUserCommand>, AddUserCommandValidator>();
          services.AddTransient<IValidator<LoginCommand>, LoginCommandValidator>();
          services.AddTransient<IValidator<SetRoleCommand>, SetRoleCommandValidator>();
          services.AddTransient<IValidator<AddAuthorCommand>, AddAuthorCommandValidator>();
          services.AddTransient<IValidator<GetBookQuery>, GetBookQueryValidator>();
          services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
     }
}