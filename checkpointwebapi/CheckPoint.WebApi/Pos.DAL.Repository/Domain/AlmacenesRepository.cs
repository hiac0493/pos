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
    public class AlmacenesRepository : GenericRepository<Almacenes>, IAlmacenesRepository
    {
        public AlmacenesRepository(PosDbContext context) : base(context) { }
        public PosDbContext dbContext
        {
            get { return _context as PosDbContext; }
        }

        public IEnumerable<object> GetProductosByAlmacen(int idAlmacen)
        {
            return dbContext.ProductoAlmacen.Include(x => x.Producto)
                .Where(x => x.idAlmacen.Equals(idAlmacen))
                .Select(a => new
                {
                    NombreProducto = a.Producto.NombreProducto,
                    Existencia = a.Existencia
                }).ToList();
        }
    }
}
