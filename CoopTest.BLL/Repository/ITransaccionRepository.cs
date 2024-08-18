using CoopTest.DAL.Models;

namespace CoopTest.BLL.Repository
{
    public interface ITransaccionRepository : IGenericRepository<Transaccion>
    {
        Task<IEnumerable<Transaccion>> GetTransaccionesPorClienteAsync(string clienteId);
    }
}
