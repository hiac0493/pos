using Pos.BAL.Interface.Domain;
using Pos.Business.Model;
using Pos.EntityFramework.Edbm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pos.DAL.Repository.Domain
{
    public class AlmacenesRepository : GenericRepository<Almacenes>, IAlmacenesRepository
    {
        public AlmacenesRepository(PosDbContext context) : base(context) { }
        public PosDbContext dbContext
        {
            get { return _context as PosDbContext; }
        }

        public IEnumerable<object> GetAllAlmacenes()
        {
            return dbContext.Almacenes.ToList();
        }
    }
}
