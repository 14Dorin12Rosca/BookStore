using BookStore.Application.Features.Author.Commands.Add;
using BookStore.Application.Features.Book.Commands.Add;
using BookStore.Application.Features.Book.Commands.Buy;
using BookStore.Application.Features.Book.Commands.Delete;
using BookStore.Application.Features.Book.Commands.Supply;
using BookStore.Application.Features.Book.Commands.Update;
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
          //ValidationForRole
          services.AddTransient<IValidator<AddRoleCommand>, AddRoleCommandValidator>();
          services.AddTransient<IValidator<GetRoleQuery>, GetRoleQueryValidator>();
          //Validation For User
          services.AddTransient<IValidator<AddUserCommand>, AddUserCommandValidator>();
          services.AddTransient<IValidator<LoginCommand>, LoginCommandValidator>();
          services.AddTransient<IValidator<SetRoleCommand>, SetRoleCommandValidator>();
          //Validation for Author
          services.AddTransient<IValidator<AddAuthorCommand>, AddAuthorCommandValidator>();
          //Validation for books
          services.AddTransient<IValidator<GetBookQuery>, GetBookQueryValidator>();
          services.AddTransient<IValidator<AddBookCommand>, AddBookCommandValidator>();
          services.AddTransient<IValidator<UpdateBookCommand>, UpdateBookCommandValidator>();
          services.AddTransient<IValidator<DeleteBookCommand>, DeleteBookCommandValidator>();
          services.AddTransient<IValidator<SupplyBookCommand>, SupplyBookCommandValidator>();
          services.AddTransient<IValidator<BuyBookCommand>,BuyBookCommandValidator>();
          services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
     }
}