using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;

namespace Pos.Business.Model
{
    public class Productos
    {
        public Productos()
        {
            this.Impuestos = new List<ImpuestoProductos>();
            this.PLUs = new List<PLUProductos>();
            this.Proveedores = new List<ProductosProveedor>();
            this.Ventas = new List<ProductosVenta>();
            this.Compras = new List<ProductosCompra>();
            this.Ordenes = new List<ProductosOrden>();
            this.Lotes = new List<VentaLote>();
            this.Almacenes = new List<ProductoAlmacen>();
            this.Promociones = new List<ProductosPromocion>();
        }
        public int idProducto { get; set; }
        [Required]
        public string NombreProducto { get; set; }
        [Required]
        public float PrecioCosto { get; set; }
        [Required]
        public float PrecioVenta { get; set; }
        public float? PrecioVentaSinImpuestos { get; set; }
        public int idMarca { get; set; }
        public Marca Marca { get; set; }
        public int idDepartamento { get; set; }
        public int idSubDepartamento { get; set; }
        public float Ganancia { get; set; }
        public Unidades Unidad { get; set; }
        public int idUnidad { get; set; }
        [Required]
        public float Minimo { get; set; }
        [Required]
        public float Maximo { get; set; }
        [Required]
        public float MinimoCompra { get; set; }
        public CatalogoSat CatalogoSat { get; set; }
        public int? idCatalogoSat { get; set; }
        public UnidadSat UnidadSat { get; set; }
        public int? idUnidadSat { get; set; }
        public Departamentos Departamento { get; set; }
        public SubDepartamento SubDepartamento { get; set; }
        public string ImagenId { get; set; }
        public virtual ICollection<ImpuestoProductos> Impuestos { get; set; }
        public virtual ICollection<PLUProductos> PLUs { get; set; }
        public virtual ICollection<ProductosProveedor> Proveedores { get; set; }
        public virtual ICollection<ProductosVenta> Ventas { get; set; }
        public virtual ICollection<ProductosCompra> Compras { get; set; }
        public virtual ICollection<ProductosOrden> Ordenes { get; set; }
        public virtual ICollection<VentaLote> Lotes { get; set; }
        public virtual ICollection<ProductoAlmacen> Almacenes { get; set; }
        public virtual ICollection<ProductosPromocion> Promociones { get; set; }
    }
}
