namespace BookStore.Application.Contracts.DataBase
{
     public interface IAddEntity<TEntity> where TEntity : class 
     {
          Task<TEntity?> InsertAsync(TEntity entity);
     }
}
