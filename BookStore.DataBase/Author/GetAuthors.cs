using BookStore.Application.Contracts.Author;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataBase.Author
{
     public class GetAuthors : IGetAuthors
     {
          private readonly IDbContextFactory<BookStoreDbContext> _factory;

          public GetAuthors(IDbContextFactory<BookStoreDbContext> factory)
          {
               _factory = factory;
          }

          public async Task<IEnumerable<Domain.Entities.Author>> GetAsync()
          {
               var ctx = await _factory.CreateDbContextAsync();
               var authors = await ctx.Author.Include(u => u.Books)
                    .Include(g => g.Books) // Include the Books navigation property here
                    .ToListAsync();
               return authors;
          }
     }
}
