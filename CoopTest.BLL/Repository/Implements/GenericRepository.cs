using CoopTest.DAL.Models;
using MongoDB.Driver;

namespace CoopTest.BLL.Repository.Implements
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly IMongoCollection<TEntity> _collection;

        public GenericRepository(CoopTestContext context, string collectionName)
        {
            _collection = context.GetCollection<TEntity>(collectionName);
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", id);
            var result = await _collection.DeleteOneAsync(filter);

            if (result.DeletedCount == 0)
                throw new Exception("The entity could not be deleted");
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _collection.Find(FilterDefinition<TEntity>.Empty).ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _collection.Find(FilterDefinition<TEntity>.Empty).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(string id, TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", id);
            var result = await _collection.ReplaceOneAsync(filter, entity);

            if (result.ModifiedCount == 0)
                throw new Exception("The entity could not be updated");

            return entity;
        }
    }

}
