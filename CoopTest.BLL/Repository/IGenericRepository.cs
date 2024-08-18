namespace CoopTest.BLL.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(string id);
        Task<TEntity> InsertAsync(TEntity entity);
        Task<TEntity> UpdateAsync(string id, TEntity entity);
        Task DeleteAsync(string id);

        IEnumerable<TEntity> GetAll();
    }

}
