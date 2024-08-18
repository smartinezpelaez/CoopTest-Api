using CoopTest.DAL.Models;
using MongoDB.Driver;

namespace CoopTest.BLL.Repository.Implements
{
    public class FondoVinculadoRepository : GenericRepository<FondoVinculado>, IFondoVinculadoRepository
    {
        public FondoVinculadoRepository(CoopTestContext context) : base(context, "FondosVinculados")
        {
        }

        public async Task<IEnumerable<FondoVinculado>> GetFondosPorClienteAsync(string clienteId)
        {
            return await _collection.Find(fv => fv.IdFondo == clienteId).ToListAsync();
        }
    }
}
