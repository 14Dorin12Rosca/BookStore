namespace BookStore.Application.Contracts.User
{
     public interface ISetUserRole
     {
          Task<Domain.Entities.User?> SetAsync(string? userName, Guid roleId);
     }
}
