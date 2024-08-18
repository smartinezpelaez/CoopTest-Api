using CoopTest.DAL.Models;

namespace CoopTest.BLL.Repository
{
    public interface IFondoRepository: IGenericRepository<Fondo>
    {
        Task<Fondo> GetFondoByNombreAsync(string nombre);
    }
}
