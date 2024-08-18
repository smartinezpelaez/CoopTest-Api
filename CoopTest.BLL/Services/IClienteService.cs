using CoopTest.BLL.DTOs;
using CoopTest.DAL.Models;

namespace CoopTest.BLL.Services
{
    public interface IClienteService
    {
        Task SuscribirClienteAFondoAsync(SuscripcionFondoDTO suscripcionFondoDTO);
        Task CrearOActualizarClienteAsync(ClienteDTO clienteDTO);       
        Task CancelarSuscripcionAsync(string clienteId, string fondoId);
        Task<IEnumerable<Transaccion>> VerHistorialTransaccionesAsync(string clienteId);        
    }

}
