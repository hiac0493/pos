using Microsoft.EntityFrameworkCore;
using Pos.BAL.Interface.Domain;
using Pos.Business.Model;
using Pos.EntityFramework.Edbm;
using System.Linq;

namespace Pos.DAL.Repository.Domain
{
    public class PromocionesRepository :GenericRepository<Promociones>, IPromocionesRepository
    {
        public PromocionesRepository(PosDbContext context) : base(context) { }
        public PosDbContext dbContext
        {
            get { return _context as PosDbContext; }
        }

        public object GetPromocionById(long idPromocion)
        {
            return dbContext.Promociones.Include(t => t.Productos).Where(x => x.idPromocion.Equals(idPromocion)).Select(a => new
            {
                idPromocion = a.idPromocion,
                NombrePromocion = a.NombrePromocion,
                Inicio = a.Inicio,
                Fin = a.Fin,
                Monto = a.Monto,
                Porcentaje = a.Porcentaje,
                idDepartamento = a.idDepartamento,
                idMarca = a.idMarca,
                DiasPromocion = a.DiasPromocion,
                Estatus = a.Estatus,
                Productos = a.Productos.Select(b => new { idProductoPromocion = b.idProductoPromocion, plu = b.Producto.PLUs.FirstOrDefault().PLU, Nombre = b.Producto.NombreProducto, idPromocion = a.idPromocion, idProducto = b.idProducto, Cantidad = b.Cantidad }).ToList()
            }).SingleOrDefault();
        }
    }
}
