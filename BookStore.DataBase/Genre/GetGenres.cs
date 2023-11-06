
using BookStore.Application.Contracts.Genre;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataBase.Genre
{
     internal class GetGenres : IGetGenres
     {
          private readonly IDbContextFactory<BookStoreDbContext> _factory;

          public GetGenres(IDbContextFactory<BookStoreDbContext> factory)
          {
               _factory = factory;
          }

          public async Task<IEnumerable<Domain.Entities.Genre>> GetAsync()
          {
               var ctx = await _factory.CreateDbContextAsync();
               var genres = await ctx.Genre.Include(u => u.Books)
                    .Include(g => g.Books) // Include the Books navigation property here
                    .ToListAsync();
               return genres;
          }
     }
}