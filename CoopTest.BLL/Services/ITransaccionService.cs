using CoopTest.DAL.Models;

namespace CoopTest.BLL.Services
{
    public interface ITransaccionService
    {
        Task<IEnumerable<Transaccion>> VerHistorialTransaccionesAsync(string clienteId);
    }
}
