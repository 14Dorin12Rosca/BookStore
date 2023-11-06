using BookStore.Application.Contracts.DataBase;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataBase.Common
{
     public class AddEntity<TEntity> :IAddEntity<TEntity> where TEntity : class
     {
          private readonly IDbContextFactory<BookStoreDbContext> _factory;

          public AddEntity(IDbContextFactory<BookStoreDbContext> factory)
          {
               _factory = factory;
          }

          public async Task<bool> InsertAsync(TEntity entity)
          {
               await using var ctx = await _factory.CreateDbContextAsync(); 
               await ctx.Set<TEntity>().AddAsync(entity);
               var affectedRows = await ctx.SaveChangesAsync();
               return affectedRows > 0;
          }
     }
}
