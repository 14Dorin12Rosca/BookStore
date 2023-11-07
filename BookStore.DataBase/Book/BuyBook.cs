using BookStore.Application.Contracts.Book;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataBase.Book
{
     internal class BuyBook :IBuyBook
     {
          private readonly IDbContextFactory<BookStoreDbContext> _factory;

          public BuyBook(IDbContextFactory<BookStoreDbContext> factory)
          {
               _factory = factory;
          }

          public async Task<bool> BuyAsync(Guid bookId, string userEmail)
          {
               var ctx = await _factory.CreateDbContextAsync();
               var user = await ctx.User.FirstOrDefaultAsync(u => u.Email == userEmail);
               if (user == null)
               {
                    throw new KeyNotFoundException("User with such email does not exist");
               }

               var book = await ctx.Book.FirstOrDefaultAsync(b => b.Id == bookId);
               if (book == null)
               {
                    throw new KeyNotFoundException("book with such id does not exist");
               }

               user.Books = new List<Domain.Entities.Book>();
               user.Books.Add(book);
               var affectedRows = await  ctx.SaveChangesAsync();
               return affectedRows > 0;
          }
     }
}
