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
    public class ProveedoresRepository : GenericRepository<Proveedores>, IProveedoresRepository
    {
        public ProveedoresRepository(PosDbContext context) : base(context) { }
        public PosDbContext dbContext
        {
            get { return _context as PosDbContext; }
        }

        public IEnumerable<Proveedores> GetSuppliersByName(string name)
        {
            name = String.IsNullOrEmpty(name) ? string.Empty : name.Trim();
            name = name.Replace(' ', '%');
            name = "%" + name + "%";
            return dbContext.Proveedores
                .Where(x => (EF.Functions.Like(x.Nombre.ToLower(), name.ToLower())))
                .ToList();
        }

        public object GetSupplierWithProductsById(int supplierId)
        {
            return dbContext.Proveedores.Include(x => x.Productos).ThenInclude(x => x.Producto).Where(x => x.idProveedor.Equals(supplierId))
                .Select(a => new
                {
                    idProveedor = a.idProveedor,
                    nombre = a.Nombre,
                    representante = a.Representante,
                    telefonos = a.Telefonos,
                    correos = a.Correos,
                    paginaWeb = a.PaginaWeb,
                    comentarios = a.Comentarios,
                    estatus = a.Estatus,
                    productosWithDepartment = (from producto in a.Productos select new { idProveedor = a.idProveedor, idProducto = producto.idProducto, pluProducto = producto.Producto.PLUs.FirstOrDefault().PLU, idDepartamento = producto.Producto.idDepartamento, descripcion = producto.Producto.NombreProducto }).ToList(),
                    productos = a.Productos
                }).FirstOrDefault();
        }

        public IEnumerable<object> GetSuppliersWithProducts()
        {
            return dbContext.Proveedores.Include(x => x.Productos).ThenInclude(x => x.Producto).Where(x => x.Estatus)
                .Select(a => new
                {
                    idProveedor = a.idProveedor,
                    nombre = a.Nombre,
                    representante = a.Representante,
                    telefonos = a.Telefonos,
                    correos = a.Correos,
                    paginaWeb = a.PaginaWeb,
                    comentarios = a.Comentarios,
                    estatus = a.Estatus,
                    productosWithDepartment = (from producto in a.Productos select new { idProveedor = a.idProveedor, idProducto = producto.idProducto, pluProducto = producto.Producto.PLUs.FirstOrDefault().PLU, idDepartamento = producto.Producto.idDepartamento, descripcion = producto.Producto.NombreProducto }).ToList(),
                    productos = a.Productos
                }).ToList();
        }
    }
}
