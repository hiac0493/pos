using Pos.BAL.Interface.Domain;
using Pos.Business.Model;
using Pos.EntityFramework.Edbm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.DAL.Repository.Domain
{
    public class TurnosRepository : GenericRepository<Turnos>, ITurnosRepository
    {
        public TurnosRepository(PosDbContext context) : base(context) { }
        public PosDbContext dbContext
        {
            get { return _context as PosDbContext; }
        }

    }
}
