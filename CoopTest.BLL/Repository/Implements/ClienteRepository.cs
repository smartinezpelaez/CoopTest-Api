using CoopTest.DAL.Models;
using MongoDB.Driver;

namespace CoopTest.BLL.Repository.Implements
{
    public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(CoopTestContext context) : base(context, "clientes") { }

        public async Task<Cliente> GetClienteWithFondosAsync(string clienteId)
        {
            return await _collection.Find(c => c.Id == clienteId).FirstOrDefaultAsync();
        }

        
    }
}
