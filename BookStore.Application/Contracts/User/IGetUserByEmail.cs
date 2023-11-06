namespace BookStore.Application.Contracts.User
{
     public interface IGetUserByEmail
     {
          Task<Domain.Entities.User?> GetAsync(string email);

     }
}
