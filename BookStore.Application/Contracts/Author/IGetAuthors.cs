namespace BookStore.Application.Contracts.Author
{
     public interface IGetAuthors
     {
          Task<IEnumerable<Domain.Entities.Author>> GetAsync();

     }
}
