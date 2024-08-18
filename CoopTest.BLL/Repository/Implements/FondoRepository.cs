using CoopTest.DAL.Models;
using MongoDB.Driver;

namespace CoopTest.BLL.Repository.Implements
{
    public class FondoRepository : GenericRepository<Fondo>, IFondoRepository
    {
        public FondoRepository(CoopTestContext context) : base(context, "fondos") { }

        public async Task<Fondo> GetFondoByNombreAsync(string nombre)
        {
            return await _collection.Find(f => f.Nombre == nombre).FirstOrDefaultAsync();
        }
        
    }
}
