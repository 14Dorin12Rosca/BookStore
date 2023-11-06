namespace BookStore.Application.Contracts.DataBase
{
     public interface IDeleteEntity<TEntity> where TEntity : class 
     {
          Task<bool> DeleteAsync(TEntity entity);
     }
}
