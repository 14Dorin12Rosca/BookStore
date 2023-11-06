namespace BookStore.Application.Contracts.Genre
{
     public interface IGetGenres
     {
          Task<IEnumerable<Domain.Entities.Genre>> GetAsync();
     }
}
