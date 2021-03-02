using Microsoft.EntityFrameworkCore;
using Pos.BAL.Interface.Domain;
using Pos.Business.Model;
using Pos.EntityFramework.Edbm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pos.DAL.Repository.Domain
{
    public class ProductosRepository : GenericRepository<Productos>, IProductosRepository
    {
        public ProductosRepository(PosDbContext context) : base(context) { }
        public PosDbContext dbContext
        {
            get { return _context as PosDbContext; }
        }

        public IEnumerable<object> GetAllProductsByDepartment(int idDepartamento)
        {
            return dbContext.Productos.Include(x=>x.PLUs).Where(x => x.idDepartamento.Equals(idDepartamento))
                .Select(a => new
                {
                    idProducto = a.idProducto,
                    pluProducto = a.PLUs.First().PLU,
                    idDepartamento = a.idDepartamento,
                    descripcion = a.NombreProducto
                }).ToList();
        }

        public Productos GetProductObjectByPLU(string plu)
        {
            int? idProduct = dbContext.PLUProductos.Where(x => x.PLU.Equals(plu)).Select(x => x.idProducto).FirstOrDefault();
            return dbContext.Productos.Include(x => x.Impuestos).ThenInclude(i=>i.Impuesto).Include(x => x.PLUs).Include(x => x.Proveedores).ThenInclude(y=>y.Proveedor)
                .Where(x => x.idProducto.Equals(idProduct)).FirstOrDefault();
        }

        public IEnumerable<object> GetAllProductsByDepartment()
        {
            return dbContext.Productos.Include(x => x.PLUs)
                .Select(a => new
                {
                    idProducto = a.idProducto,
                    pluProducto = a.PLUs.First().PLU,
                    idDepartamento = a.idDepartamento,
                    descripcion = a.NombreProducto
                }).ToList();
        }

        public IEnumerable<object> GetProductosByCriteria(string searchCriteria)
        {
            searchCriteria = String.IsNullOrEmpty(searchCriteria) ? string.Empty : searchCriteria.Trim();
            searchCriteria = searchCriteria.Replace(' ', '%');
            searchCriteria = $"%{searchCriteria}%";
            return dbContext.Productos.Include(x=>x.PLUs)
                .Where(x => EF.Functions.Like(x.NombreProducto.ToLower(), searchCriteria.ToLower())
                || x.PLUs.Where(y => EF.Functions.Like(y.PLU, searchCriteria)).SingleOrDefault() != null
                )
                .Select(a => new
                {
                    idProducto = a.idProducto,
                    pluProducto = a.PLUs.FirstOrDefault().PLU,
                    nombre = a.NombreProducto,
                    existencia = a.Existencia,
                    precioVenta = a.PrecioVenta
                })
                .Take(5)
                .ToList();
        }

        public object GetProductosWithSuppliers(int idProduct)
         {
            return dbContext.Productos.Include(x=>x.Impuestos).ThenInclude(i=>i.Impuesto).Include(x => x.Proveedores).ThenInclude(a=>a.Proveedor)
                .Where(x => x.idProducto.Equals(idProduct))
                .Select(a => new
                {
                    idProducto = a.idProducto,
                    description = a.NombreProducto,
                    supplier = (from proveedor in a.Proveedores where proveedor.Proveedor.Estatus select new { idProveedor = proveedor.Proveedor.idProveedor, nombreProveedor = proveedor.Proveedor.Nombre}).ToArray(),
                    impuestos = (from impuesto in a.Impuestos select new { idImpuesto = impuesto.idImpuesto , descripcion = impuesto.Impuesto.Descripcion, porcentaje = impuesto.Impuesto.Porcentaje}).ToList(),
                    price = a.PrecioCosto
                }).SingleOrDefault();
        }

        public IEnumerable<object> GetProductsSuggestToBuy(int supplier)
        {
            List<int> productosSupplier = dbContext.ProductosProveedor.Where(x => x.idProveedor.Equals(supplier)).Select(x => x.idProducto).ToList();
            return dbContext.Productos.Include(x => x.Impuestos).ThenInclude(i => i.Impuesto).Include(x => x.Proveedores).ThenInclude(a => a.Proveedor)
                .Where(x => productosSupplier.Contains(x.idProducto) && x.Existencia <= x.Maximo - x.MinimoCompra)
                .Select(a => new
                {
                    quantity = (Math.Floor((a.Maximo - a.Existencia) / a.MinimoCompra)) * a.MinimoCompra,
                    idProducto = a.idProducto,
                    description = a.NombreProducto,
                    supplier = a.Proveedores.Where(x => x.idProveedor.Equals(supplier)).Select(s => new { idProveedor = s.idProveedor, nombreProveedor = s.Proveedor.Nombre}).ToArray(),
                    impuestos = (from impuesto in a.Impuestos select new { idImpuesto = impuesto.idImpuesto, descripcion = impuesto.Impuesto.Descripcion, porcentaje = impuesto.Impuesto.Porcentaje }).ToList(),
                    price = a.PrecioCosto,
                    total = (Math.Floor((a.Maximo - a.Existencia) / a.MinimoCompra)) * a.MinimoCompra * a.PrecioCosto
                }).ToList();
        }

        public IEnumerable<object> GetProductsSuggestToBuy()
        {
            return dbContext.Productos.Include(x => x.Impuestos).ThenInclude(i => i.Impuesto).Include(x => x.Proveedores).ThenInclude(a => a.Proveedor)
                .Where(x=> x.Existencia <= x.Maximo - x.MinimoCompra)
                .Select(a => new
                {
                    quantity = (Math.Floor((a.Maximo - a.Existencia) / a.MinimoCompra)) * a.MinimoCompra,
                    idProducto = a.idProducto,
                    description = a.NombreProducto,
                    supplier = (from proveedor in a.Proveedores where proveedor.Proveedor.Estatus select new { idProveedor = proveedor.Proveedor.idProveedor, nombreProveedor = proveedor.Proveedor.Nombre }).ToArray(),
                    impuestos = (from impuesto in a.Impuestos select new { idImpuesto = impuesto.idImpuesto, descripcion = impuesto.Impuesto.Descripcion, porcentaje = impuesto.Impuesto.Porcentaje }).ToList(),
                    price = a.PrecioCosto,
                    total = (Math.Floor((a.Maximo - a.Existencia) / a.MinimoCompra)) * a.MinimoCompra * a.PrecioCosto
                }).ToList();
        }

        public IEnumerable<object> GetSuggestionProductsToBuyBySupplier(int supplier)
        {
            return dbContext.Productos.Include(x => x.Impuestos).ThenInclude(i => i.Impuesto).Include(x => x.Proveedores).ThenInclude(a => a.Proveedor)
                .Select(a => new
                {
                    idProducto = a.idProducto,
                    description = a.NombreProducto,
                    supplier = (from proveedor in a.Proveedores where proveedor.Proveedor.Estatus select new { idProveedor = proveedor.Proveedor.idProveedor, nombreProveedor = proveedor.Proveedor.Nombre }).ToArray(),
                    impuestos = (from impuesto in a.Impuestos select new { idImpuesto = impuesto.idImpuesto, descripcion = impuesto.Impuesto.Descripcion, porcentaje = impuesto.Impuesto.Porcentaje }).ToList(),
                    price = a.PrecioCosto
                }).ToList();
        }

        public IEnumerable<object> GetProductsByList(List<ProductosOrden> productos)
        {
            return dbContext.Productos.Include(x => x.Impuestos)
                .Where(x => productos.Select(s => s.idProducto).Contains(x.idProducto))
                .Select(a => new
                {
                    idProducto = a.idProducto,
                    description = a.NombreProducto,
                    supplier = (from proveedor in a.Proveedores where proveedor.Proveedor.Estatus select new { idProveedor = proveedor.Proveedor.idProveedor, nombreProveedor = proveedor.Proveedor.Nombre }).ToArray(),
                    impuestos = (from impuesto in a.Impuestos select new { idImpuesto = impuesto.idImpuesto, descripcion = impuesto.Impuesto.Descripcion, porcentaje = impuesto.Impuesto.Porcentaje }).ToList(),
                    price = a.PrecioCosto
                }).ToList();
        }

        public IEnumerable<Productos> GetAllProducts()
        {
            return dbContext.Productos.Include(x => x.PLUs).Include(x=>x.Impuestos).ThenInclude(a=>a.Impuesto).Include(x=>x.Proveedores).ThenInclude(p=>p.Proveedor).OrderBy(x => x.idProducto);
        }
    }
}