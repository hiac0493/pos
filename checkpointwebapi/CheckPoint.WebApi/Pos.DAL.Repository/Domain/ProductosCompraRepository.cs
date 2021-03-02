using Pos.BAL.Interface.Domain;
using Pos.Business.Model;
using Pos.EntityFramework.Edbm;

namespace Pos.DAL.Repository.Domain
{
    public class ProductosCompraRepository : GenericRepository<ProductosCompra>, IProductosCompraRepository
    {
        public ProductosCompraRepository(PosDbContext context) : base(context) { }
        public PosDbContext DbContext
        {
            get { return _context as PosDbContext; }
        }
    }
}
