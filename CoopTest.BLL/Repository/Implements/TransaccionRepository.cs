using CoopTest.DAL.Models;
using MongoDB.Driver;

namespace CoopTest.BLL.Repository.Implements
{
    public class TransaccionRepository : GenericRepository<Transaccion>, ITransaccionRepository
    {
        public TransaccionRepository(CoopTestContext context) : base(context, "transacciones") { }

        public async Task<IEnumerable<Transaccion>> GetTransaccionesPorClienteAsync(string clienteId)
        {
            return await _collection.Find(t => t.IdCliente == clienteId).ToListAsync();
        }
        
    }

}
