using BookStore.Application.Contracts.DataBase;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataBase.Common
{
     internal class SelectAllAsQueryable<TEntity>:ISelectAllAsQueryable<TEntity> where TEntity : class
     {
          private readonly IDbContextFactory<BookStoreDbContext> _factory;

          public SelectAllAsQueryable(IDbContextFactory<BookStoreDbContext> factory)
          {
               _factory = factory;
          }

          public async Task<IQueryable<TEntity>> GetAll()
          {
               await using var ctx =await _factory.CreateDbContextAsync();
               return ctx.Set<TEntity>().AsQueryable();
          }
     }
}
