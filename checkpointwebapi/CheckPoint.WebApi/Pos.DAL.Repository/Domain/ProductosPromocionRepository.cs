using Pos.BAL.Interface.Domain;
using Pos.Business.Model;
using Pos.EntityFramework.Edbm;

namespace Pos.DAL.Repository.Domain
{
    public class ProductosPromocionRepository : GenericRepository<ProductosPromocion>, IProductosPromocionRepository
    {
        public ProductosPromocionRepository(PosDbContext context) : base(context) { }
        public PosDbContext dbContext
        {
            get { return _context as PosDbContext; }
        }

    }
}
