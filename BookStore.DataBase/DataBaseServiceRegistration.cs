using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using BookStore.Application.Contracts.DataBase;
using BookStore.DataBase.Common;
using Microsoft.EntityFrameworkCore;

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
               return services;
          }
     }
}
