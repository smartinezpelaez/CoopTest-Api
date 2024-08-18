using CoopTest.BLL.Repository;
using CoopTest.DAL.Models;

namespace CoopTest.BLL.Services.Implements
{
    public class TransaccionService : ITransaccionService
    {
        private readonly ITransaccionRepository _transaccionRepository;

        public TransaccionService(ITransaccionRepository transaccionRepository)
        {
            _transaccionRepository = transaccionRepository;
        }

        public async Task<IEnumerable<Transaccion>> VerHistorialTransaccionesAsync(string clienteId)
        {
            return await _transaccionRepository.GetTransaccionesPorClienteAsync(clienteId);
        }
    }

}
