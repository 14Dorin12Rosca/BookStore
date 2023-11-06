using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace BookStore.DataBase
{
     public static class DataBaseServiceRegistration
     {
          public static IServiceCollection AddDatabaseServices(this IServiceCollection services, ConfigurationManager config)
          {

               return services;
          }
     }
}
