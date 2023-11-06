using BookStore.Application.Contracts.DataBase;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataBase.Common
{
     internal class SelectAll<TEntity> : ISelectAll<TEntity> where TEntity : class
     {
          private readonly IDbContextFactory<BookStoreDbContext> _factory;

          public SelectAll(IDbContextFactory<BookStoreDbContext> factory)
          {
               _factory = factory;
          }

          public async Task<IEnumerable<TEntity>> GetAsync()
          {
               await using var ctx = await _factory.CreateDbContextAsync();
               return await ctx.Set<TEntity>().ToListAsync();
          }
     }
}
