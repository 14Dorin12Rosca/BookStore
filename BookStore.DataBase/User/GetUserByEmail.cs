using BookStore.Application.Contracts.User;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataBase.User
{
     internal class GetUserByEmail :IGetUserByEmail
     {
          private readonly IDbContextFactory<BookStoreDbContext> _factory;

          public GetUserByEmail(IDbContextFactory<BookStoreDbContext> factory)
          {
               _factory = factory;
          }

          public async Task<Domain.Entities.User?> GetAsync(string email)
          {
               await using var ctx = await _factory.CreateDbContextAsync();
               return await ctx.User.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == email);
          }
     }
}
