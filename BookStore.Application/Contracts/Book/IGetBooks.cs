namespace BookStore.Application.Contracts.Book
{
     public interface IGetBooks
     {
          Task<IEnumerable<Domain.Entities.Book>> GetAsync();

     }
}
