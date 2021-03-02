using Microsoft.EntityFrameworkCore;
using Pos.BAL.Interface.Domain;
using Pos.Business.Model;
using Pos.EntityFramework.Edbm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pos.DAL.Repository.Domain
{
    public class CortesRepository : GenericRepository<Cortes>, ICortesRepository
    {
        public CortesRepository(PosDbContext context) : base(context) { }
        public PosDbContext dbContext
        {
            get { return _context as PosDbContext; }
        }

        public object GetCorteInfo(Cortes corte)
        {
            return dbContext.Cortes.Include(x => x.Caja).Include(y => y.Turno)
                .Where(x => x.IdCaja.Equals(corte.IdCaja))
                .Where(y => y.IdTurno.Equals(corte.IdTurno))
                .Where(x => x.IdUsuario.Equals(corte.IdUsuario))
                .Select(a => new
                {
                    idCorte = a.IdCorte,
                    FechaInicio = a.FechaInicio,
                    FechaFinal = a.FechaFinal,
                    TotalVenta = a.TotalVenta,
                    TotalUtilidad = a.TotalUtilidad,
                    Caja = a.Caja,
                    Turno = a.Turno
                }).FirstOrDefault();
        }

        public Cortes GetCurrentCashClose(int usuario)
        {
            return dbContext.Cortes.Where(x => x.IdUsuario.Equals(usuario)).OrderByDescending(x => x.IdCorte).FirstOrDefault();
        }
    }
}
