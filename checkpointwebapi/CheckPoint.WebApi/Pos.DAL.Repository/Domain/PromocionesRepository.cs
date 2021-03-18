using Pos.BAL.Interface.Domain;
using Pos.Business.Model;
using Pos.EntityFramework.Edbm;

namespace Pos.DAL.Repository.Domain
{
    public class PromocionesRepository :GenericRepository<Promociones>, IPromocionesRepository
    {
        public PromocionesRepository(PosDbContext context) : base(context) { }
        public PosDbContext dbContext
        {
            get { return _context as PosDbContext; }
        }

    }
}
