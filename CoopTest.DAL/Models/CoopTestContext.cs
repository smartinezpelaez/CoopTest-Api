using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace CoopTest.DAL.Models
{
    public class CoopTestContext
    {
        private readonly IMongoDatabase _database;

        public CoopTestContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["MongoDB:ConnectionString"]);
            _database = client.GetDatabase(configuration["MongoDB:DatabaseName"]);
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>(string collectionName)
        {
            return _database.GetCollection<TEntity>(collectionName);
        }


        public IMongoCollection<Cliente> Clientes => _database.GetCollection<Cliente>("Clientes");
        public IMongoCollection<Fondo> Fondos => _database.GetCollection<Fondo>("Fondos");
        public IMongoCollection<Transaccion> Transacciones => _database.GetCollection<Transaccion>("Transacciones");
    }
}
