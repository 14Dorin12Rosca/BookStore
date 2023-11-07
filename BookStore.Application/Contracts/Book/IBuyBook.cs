using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Contracts.Book
{
     public interface IBuyBook
     {
          Task<bool> BuyAsync(Guid bookId,string userEmail );

     }
}
