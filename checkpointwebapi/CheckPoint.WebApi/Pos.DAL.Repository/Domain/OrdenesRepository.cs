using Microsoft.EntityFrameworkCore;
using Pos.BAL.Interface.Domain;
using Pos.Business.Model;
using Pos.EntityFramework.Edbm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Pos.DAL.Repository.Domain
{
    public class OrdenesRepository : GenericRepository<Ordenes>, IOrdenesRepository
    {
        public OrdenesRepository(PosDbContext context) : base(context) { }
        public PosDbContext dbContext
        {
            get { return _context as PosDbContext; }
        }

        public IEnumerable<object> GetAllOrdersWithSupplier()
        {
            object[] estatus = { new { descripcion = "Elaborada", estatus = 'E' }, new { descripcion = "Entregada", estatus = 'S' }, new { descripcion = "Cancelada", estatus = 'C' } };
            return dbContext.Ordenes.Include(x => x.Proveedor)
                .Select(a => new
                {
                    idOrden = a.idOrden,
                    estatus = a.Estatus,
                    estatusOrders = estatus,
                    idProveedor = a.idProveedor,
                    proveedor = a.Proveedor.Nombre,
                    numeroArticulos = a.NumeroArticulos,
                    total = a.Total,
                    idCompra = a.idCompra,
                    fechaCreacion = a.FechaCreacion,
                    ultimaModificacion = a.UltimaModificacion,
                    usuarioAutoriza = a.UsuarioAutoriza,
                    fechaAutorizacion = a.FechaAutorizacion,
                    productosOrden = a.ProductosOrden
                }).OrderByDescending(a => a.idOrden).ToList();
        }

        public IEnumerable<object> GetOrdersByEstatusWithSupplier(char estatusSearch)
        {
            object[] estatus = { new { descripcion = "Elaborada", estatus = 'E' }, new { descripcion = "Entregada", estatus = 'S' }, new { descripcion = "Cancelada", estatus = 'C' } };
            return dbContext.Ordenes.Include(x => x.Proveedor)
                .Where(x=>x.Estatus.Equals(estatusSearch))
                .Select(a => new
                {
                    idOrden = a.idOrden,
                    estatus = a.Estatus,
                    estatusOrders = estatus,
                    idProveedor = a.idProveedor,
                    proveedor = a.Proveedor.Nombre,
                    numeroArticulos = a.NumeroArticulos,
                    total = a.Total,
                    idCompra = a.idCompra,
                    fechaCreacion = a.FechaCreacion,
                    ultimaModificacion = a.UltimaModificacion,
                    usuarioAutoriza = a.UsuarioAutoriza,
                    fechaAutorizacion = a.FechaAutorizacion,
                    productosOrden = a.ProductosOrden
                }).OrderByDescending(a => a.idOrden).ToList();
        }
    }
}
