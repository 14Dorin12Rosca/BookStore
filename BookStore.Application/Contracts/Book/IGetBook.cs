namespace BookStore.Application.Contracts.Book
{
     public interface IGetBook
     {
          Task<Domain.Entities.Book?> GetAsync(Guid id);

     }
}