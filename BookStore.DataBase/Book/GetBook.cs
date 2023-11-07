using BookStore.Application.Contracts.Book;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataBase.Book
{
     public class GetBook : IGetBook
     {
          private readonly IDbContextFactory<BookStoreDbContext> _factory;

          public GetBook(IDbContextFactory<BookStoreDbContext> factory)
          {
               _factory = factory;
          }

          public async Task<Domain.Entities.Book?> GetAsync(Guid id)
          {
               var ctx = await _factory.CreateDbContextAsync();
               var book = await ctx.Book
                    .Include(u => u.Author)
                    .Include(b => b.Genre)
                    .Where(b => b.Id == id) // Filter by the specified Id
                    .FirstOrDefaultAsync();
               return book;
          }
     }
}
