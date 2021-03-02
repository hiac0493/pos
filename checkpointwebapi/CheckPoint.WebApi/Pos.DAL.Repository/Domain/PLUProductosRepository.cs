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
    public class PLUProductosRepository : GenericRepository<PLUProductos>, IPLUProductoRepository
    {
        public PLUProductosRepository(PosDbContext context) : base(context) { }

        public object GetProductoVenta(string PLU)
        {
            return dbContext.PLUProductos
                .Include(b => b.Producto).ThenInclude(b => b.Impuestos).ThenInclude(b => b.Impuesto)
                .Where(a => a.PLU.Equals(PLU))
                .Select(a => new
                {
                    idProducto = a.idProducto,
                    pluProducto = a.PLU,
                    nombre = a.Producto.NombreProducto,
                    existencia = a.Producto.Existencia,
                    precioVenta = a.Producto.PrecioVenta,
                    imagenId = a.Producto.ImagenId,
                    impuestos = a.Producto.Impuestos.Select(b => new { idImpuesto = b.idImpuesto, descripcion = b.Impuesto.Descripcion, porcentaje = b.Impuesto.Porcentaje }).ToList()
                }).SingleOrDefault();
        }

        public PosDbContext dbContext
        {
            get { return _context as PosDbContext; }
        }
    }
}
