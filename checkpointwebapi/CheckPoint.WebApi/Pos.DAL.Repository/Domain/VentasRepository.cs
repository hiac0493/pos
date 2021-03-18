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
    public class VentasRepository : GenericRepository<Ventas>, IVentasRepository
    {
        public VentasRepository(PosDbContext context) : base(context) { }
        public PosDbContext dbContext
        {
            get { return _context as PosDbContext; }
        }

        public Ventas GetVentaByFolio(long folioVenta)
        {
            return dbContext.Ventas
                .Include(t => t.Pagos).ThenInclude(x => x.TipoPago)
                .Include(t => t.Productos).ThenInclude(x => x.Productos)
                .Include(t => t.Lotes)
                .Where(t => t.FolioVenta.Equals(folioVenta))
                .FirstOrDefault();
        }

        public double GetTotalSale(int idUsuario, long folioInicio, long folioFin)
        {
            double result = dbContext.Ventas.Where(x => x.FolioVenta >= folioInicio && x.FolioVenta <= folioFin && x.idUsuario.Equals(idUsuario) && x.Estatus.Equals('A')).Sum(x => x.Total);
            return result;
        }

        public long GetLastFolio()
        {
            return dbContext.Ventas.Max(x => x.FolioVenta);
        }

        public IEnumerable<object> GetAllPagosCorte(long folioInicio, long folioFinal, int IdUsuario)
        {
            return dbContext.VentaPagos.Include(x => x.Venta).Where(x => x.idVenta >= folioInicio && x.idVenta <= folioFinal && x.Venta.idUsuario.Equals(IdUsuario) && x.Venta.Estatus.Equals('A'))
                 .GroupBy(x => x.idTipoPago)
                 .Select(a => new
                 {
                     IdTipoPago = a.Key,
                     TipoPago = dbContext.TipoPago.Where(x => x.idTipoPago.Equals(a.Key)).Select(x => x.Descripcion).SingleOrDefault(),
                     Total = a.Sum(x => x.Cantidad)
                 }).ToList();
        }

        public object GetTotalWithTaxes(long folioInicio, long folioFinal, int IdUsuario)
        {
            List<long> ventasUsuario = dbContext.Ventas.Where(x => x.FolioVenta >= folioInicio && x.FolioVenta <= folioFinal && x.idUsuario.Equals(IdUsuario) && x.Estatus.Equals('A')).Select(x => x.FolioVenta).ToList();
            List<int> impuesto = dbContext.ImpuestoProductos.Where(x => x.idImpuesto.Equals(1)).Select(x => x.idProducto).ToList();
            double total = dbContext.ProductosVenta.Include(x => x.Productos).ThenInclude(x => x.Impuestos)
                .Where(x => ventasUsuario.Contains(x.idVenta) && impuesto.Contains(x.idProducto)).Sum(x => x.Monto);
            double total2 = dbContext.ProductosVenta
                .Where(x => ventasUsuario.Contains(x.idVenta)).Sum(x => x.Monto);

            var taxes = new List<object>()
            {
                new{Nombre = "Gravado al 16%", Total= total },
                new{Nombre = "Gravado al 0%", Total= total2 - total}
            };
            return taxes;
        }

        public object GetReturnsWithTaxes(long folioInicio, long folioFinal, int IdUsuario)
        {
            List<long> ventasUsuario = dbContext.Ventas.Where(x => x.FolioVenta >= folioInicio && x.FolioVenta <= folioFinal && x.idUsuario.Equals(IdUsuario) && !x.Estatus.Equals('A')).Select(x => x.FolioVenta).ToList();
            List<int> impuesto = dbContext.ImpuestoProductos.Where(x => x.idImpuesto.Equals(1)).Select(x => x.idProducto).ToList();
            double total = dbContext.ProductosVenta.Include(x => x.Productos).ThenInclude(x => x.Impuestos)
                .Where(x => ventasUsuario.Contains(x.idVenta) && impuesto.Contains(x.idProducto)).Sum(x => x.Monto);
            double total2 = dbContext.ProductosVenta
                .Where(x => ventasUsuario.Contains(x.idVenta)).Sum(x => x.Monto);

            var taxes = new List<object>()
            {
                new{Nombre = "Gravado al 16%", Total= total },
                new{Nombre = "Gravado al 0%", Total= total2 - total}
            };
            return taxes;
        }

        public double CalcIvaTasa(int idUsuario, long folioInicio, long folioFin)
        {
            List<long> ventasUsuario = dbContext.Ventas.Where(x => x.FolioVenta >= folioInicio && x.FolioVenta <= folioFin && x.idUsuario.Equals(idUsuario) && x.Estatus.Equals('A')).Select(x => x.FolioVenta).ToList();
            List<int> impuesto = dbContext.ImpuestoProductos.Where(x => x.idImpuesto.Equals(1)).Select(x => x.idProducto).ToList();
            double total = dbContext.ProductosVenta.Include(x => x.Productos).ThenInclude(x => x.Impuestos)
                .Where(x => ventasUsuario.Contains(x.idVenta) && impuesto.Contains(x.idProducto)).Sum(x => x.Monto);
            double totalIva = total / 1.16;
            return total - totalIva; 
        }
        public double CalcIvaReturn(int idUsuario, long folioInicio, long folioFin)
        {
            List<long> ventasUsuario = dbContext.Ventas.Where(x => x.FolioVenta >= folioInicio && x.FolioVenta <= folioFin && x.idUsuario.Equals(idUsuario) && !x.Estatus.Equals('A')).Select(x => x.FolioVenta).ToList();
            List<int> impuesto = dbContext.ImpuestoProductos.Where(x => x.idImpuesto.Equals(1)).Select(x => x.idProducto).ToList();
            double total = dbContext.ProductosVenta.Include(x => x.Productos).ThenInclude(x => x.Impuestos)
                .Where(x => ventasUsuario.Contains(x.idVenta) && impuesto.Contains(x.idProducto)).Sum(x => x.Monto);
            double totalIva = total / 1.16;
            return total - totalIva;
        }

        public double GetTotalReturn(int idUsuario, long folioInicio, long folioFin)
        {
            double result = dbContext.Ventas.Where(x => x.FolioVenta >= folioInicio && x.FolioVenta <= folioFin && x.idUsuario.Equals(idUsuario) && x.Estatus.Equals('C')).Sum(x => x.Total);
            return result;
        }
    }
}
