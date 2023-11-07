using BookStore.Application.Contracts.Book;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataBase.Book
{
     public class GetBooks : IGetBooks
     {
          private readonly IDbContextFactory<BookStoreDbContext> _factory;

          public GetBooks(IDbContextFactory<BookStoreDbContext> factory)
          {
               _factory = factory;
          }

          public async Task<IEnumerable<Domain.Entities.Book>> GetAsync()
          {
               var ctx = await _factory.CreateDbContextAsync();
               var books = await ctx.Book
                    .Include(u => u.Author)
                    .Include(b => b.Genre)
                    .ToListAsync();
               return books;
          }
     }
}
