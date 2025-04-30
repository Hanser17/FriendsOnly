namespace Application.IRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAll(string userId);

        Task<TEntity> GetById(int? id);

        Task<TEntity> Add(TEntity entity);

        Task Update(TEntity entity);
        Task Delete(TEntity entity);
       
    }
}
