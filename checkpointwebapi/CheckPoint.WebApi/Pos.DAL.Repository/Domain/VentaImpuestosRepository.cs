using Microsoft.EntityFrameworkCore;
using Pos.BAL.Interface.Domain;
using Pos.Business.Model;
using Pos.EntityFramework.Edbm;
using System.Collections.Generic;
using System.Linq;

namespace Pos.DAL.Repository.Domain
{
    public class VentaImpuestosRepository : GenericRepository<VentaImpuestos>, IVentaImpuestosRepository
    {
        public VentaImpuestosRepository(PosDbContext context) : base(context) { }
        public PosDbContext dbContext
        {
            get { return _context as PosDbContext; }
        }

        public IEnumerable<object> GetAllTaxes(long folioInicio, long folioFinal, int IdUsuario)
        {
            return dbContext.VentaImpuestos.Include(x => x.Venta).Where(x => x.IdVenta >= folioInicio && x.IdVenta <= folioFinal && x.Venta.idUsuario.Equals(IdUsuario))
                .GroupBy(x => x.IdImpuesto)
                .Select(a => new
                {
                    IdImpuesto = a.Key,
                    TipoImpuesto = dbContext.Impuestos.Where(x => x.idImpuesto.Equals(a.Key)).Select(x => x.Descripcion).SingleOrDefault(),
                    Total = a.Sum(x => x.Cantidad)
                }).ToList();
        }


        public double GetTotalTaxes(long folioInicio, long folioFinal, int IdUsuario)
        {
            return dbContext.VentaImpuestos.Include(x => x.Venta).Where(x => x.IdVenta >= folioInicio && x.IdVenta <= folioFinal && x.Venta.idUsuario.Equals(IdUsuario)).Sum(x => x.Cantidad);
        }


    }
}
