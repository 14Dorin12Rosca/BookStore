namespace BookStore.Application.Contracts.DataBase
{
     public interface IUpdateEntity<TEntity> where TEntity : class 
     {
          Task<bool> UpdateAsync(TEntity entity);
     }
}
