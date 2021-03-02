using Pos.BAL.Interface.Domain;
using Pos.Business.Model;
using Pos.EntityFramework.Edbm;
using System.Collections.Generic;
using System.Linq;

namespace Pos.DAL.Repository.Domain
{
    public class UnidadesRepository : GenericRepository<Unidades>, IUnidadesRepository
    {
        public UnidadesRepository(PosDbContext context) : base(context) { }
        public PosDbContext dbContext
        {
            get { return _context as PosDbContext; }
        }

        public IEnumerable<object> GetAllUnidades()
        {
            return dbContext.Unidades
                .ToList();
        }
    }
}
