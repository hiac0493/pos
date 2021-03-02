using Pos.BAL.Interface.Domain;
using Pos.Business.Model;
using Pos.EntityFramework.Edbm;
using System.Collections.Generic;
using System.Linq;

namespace Pos.DAL.Repository.Domain
{
    public class UnidadSatRepository : GenericRepository<UnidadSat>, IUnidadSatRepository
    {
        public UnidadSatRepository(PosDbContext context) : base(context) { }
        public PosDbContext dbContext
        {
            get { return _context as PosDbContext; }
        }

        public IEnumerable<object> GetAllUnidadesSat()
        {
            return dbContext.UnidadSat.
                ToList();
        }
        public IEnumerable<object> GetUnidadesSatByid(int unitSatId)
        {
            return dbContext.UnidadSat.
                Where(x => x.idUnidadSat.Equals(unitSatId)).
                ToList();
        }
    }
}
