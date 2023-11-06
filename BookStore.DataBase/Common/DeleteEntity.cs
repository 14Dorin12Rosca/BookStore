using BookStore.Application.Contracts.DataBase;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataBase.Common
{
     internal class DeleteEntity<TEntity> :IDeleteEntity<TEntity> where TEntity : class
     {
          private readonly IDbContextFactory<BookStoreDbContext> _factory;

          public DeleteEntity(IDbContextFactory<BookStoreDbContext> factory)
          {
               _factory = factory;
          }

          public async Task<bool> DeleteAsync(TEntity entity)
          {
               await using var ctx = await _factory.CreateDbContextAsync();
               ctx.Set<TEntity>().Remove(entity);
               var affectedRows = await ctx.SaveChangesAsync();
               return affectedRows > 0;
          }
     }
}
