namespace BookStore.Application.Contracts.DataBase
{
     public interface ISelectAllAsQueryable<TEntity> where TEntity : class 
     {
          Task<IQueryable<TEntity>> GetAll();
     }
}
