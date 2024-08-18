using CoopTest.DAL.Models;

namespace CoopTest.BLL.Repository
{
    public interface IClienteRepository: IGenericRepository<Cliente>
    {
        Task<Cliente> GetClienteWithFondosAsync(string clienteId);
    }
}
