using BookStore.Application.Contracts.DataBase;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataBase.Common
{
     internal class GetEntityById<TEntity>:IGetEntityById<TEntity> where TEntity : class
     {
          private readonly IDbContextFactory<BookStoreDbContext> _factory;

          public GetEntityById(IDbContextFactory<BookStoreDbContext> factory)
          {
               _factory = factory;
          }

          public async Task<TEntity?> GetAsync<T>(T id)
          {
               await using var ctx = await _factory.CreateDbContextAsync();
               return await ctx.FindAsync<TEntity>(id);
          }

          public async Task<TEntity?> GetAsync<T, TIncluded>(T id)
          {
               await using var ctx = await _factory.CreateDbContextAsync();
               var result = ctx.Set<TEntity>().Include(e => e);
               return (TEntity?)result;
          }
     }
}
