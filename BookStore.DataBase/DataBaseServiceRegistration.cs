﻿using BookStore.Application.Contracts.Author;
using BookStore.Application.Contracts.Book;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using BookStore.Application.Contracts.DataBase;
using BookStore.Application.Contracts.Genre;
using BookStore.Application.Contracts.User;
using BookStore.DataBase.Author;
using BookStore.DataBase.Book;
using BookStore.DataBase.Common;
using BookStore.DataBase.Genre;
using Microsoft.EntityFrameworkCore;
using BookStore.DataBase.User;

namespace BookStore.DataBase
{
    public static class DataBaseServiceRegistration
     {
          public static IServiceCollection AddDatabaseServices(this IServiceCollection services, ConfigurationManager config)
          {
               services.AddDbContextFactory<BookStoreDbContext>(options =>
               {
                    options.UseSqlServer(config.GetConnectionString("BookStoreDbConnection"));
               });
               //Common
               services.AddScoped(typeof(IAddEntity<>), typeof(AddEntity<>));
               services.AddScoped(typeof(IDeleteEntity<>), typeof(DeleteEntity<>));
               services.AddScoped(typeof(IGetEntityById<>), typeof(GetEntityById<>));
               services.AddScoped(typeof(ISelectAll<>), typeof(SelectAll<>));
               services.AddScoped(typeof(ISelectAllAsQueryable<>), typeof(SelectAllAsQueryable<>));
               services.AddScoped(typeof(IUpdateEntity<>), typeof(UpdateEntity<>));
               //User
               services.AddScoped(typeof(IGetUserByEmail), typeof(GetUserByEmail));
               services.AddScoped(typeof(ISetUserRole), typeof(SetUserRole));
               //Genres
               services.AddScoped(typeof(IGetGenres), typeof(GetGenres));
               //Author
               services.AddScoped(typeof(IGetAuthors), typeof(GetAuthors));
               //Book
               services.AddScoped(typeof(IGetBooks), typeof(GetBooks));
               services.AddScoped(typeof(IGetBook), typeof(GetBook));
               services.AddScoped(typeof(IBuyBook), typeof(BuyBook));

               return services;
          }
     }
}
