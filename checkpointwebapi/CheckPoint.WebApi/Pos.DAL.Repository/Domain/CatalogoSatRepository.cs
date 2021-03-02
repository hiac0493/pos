using Pos.BAL.Interface.Domain;
using Pos.Business.Model;
using Pos.EntityFramework.Edbm;
using System.Collections.Generic;
using System.Linq;

namespace Pos.DAL.Repository.Domain
{
    public class CatalogoSatRepository : GenericRepository<CatalogoSat>, ICatalogoSatRepository
    {
        public CatalogoSatRepository(PosDbContext context) : base(context) { }
        public PosDbContext dbContext
        {
            get { return _context as PosDbContext; }
        }

        public IEnumerable<object> GetAllCatalogoSat()
        {
            return dbContext.CatalogoSat.
                ToList();
        }
    }
}
