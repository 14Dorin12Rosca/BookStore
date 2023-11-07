namespace BookStore.Application.Contracts.DataBase
{
     public interface IGetEntityById<TEntity> where TEntity : class 
     {
          Task<TEntity?> GetAsync<T>(T id);

     }
}
