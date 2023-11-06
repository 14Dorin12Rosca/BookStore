namespace BookStore.Application.Contracts.DataBase
{
     public interface ISelectAll<TEntity> where TEntity : class 
     {
          Task<IEnumerable<TEntity>> GetAsync();
     }
}
