using BookStore.Application.Contracts.DataBase;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataBase.Common
{
     internal class UpdateEntity<TEntity>:IUpdateEntity<TEntity> where TEntity : class
     {
          private readonly IDbContextFactory<BookStoreDbContext> _factory;

          public UpdateEntity(IDbContextFactory<BookStoreDbContext> factory)
          {
               _factory = factory;
          }

          public async Task<bool> UpdateAsync(TEntity entity)
          {
               await using var ctx = await _factory.CreateDbContextAsync();
               ctx.Update(entity);
               var affectedRows = await ctx.SaveChangesAsync();
               return affectedRows > 0;
          }
     }
}
