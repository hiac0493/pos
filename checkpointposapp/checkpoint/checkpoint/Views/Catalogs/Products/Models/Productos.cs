using System.Collections.Generic;

namespace checkpoint.Views.Catalogs.Products.Models
{
    public class Productos
    {
        public int idProducto { get; set; }
        public string NombreProducto { get; set; }
        public float Existencia { get; set; }
        public float PrecioCosto { get; set; }
        public float PrecioVenta { get; set; }
        public float PrecioVentaSinImpuestos { get; set; }
        public int idMarca { get; set; }
        public int idDepartamento { get; set; }
        public float Ganancia { get; set; }
        public int idUnidad { get; set; }
        public float Minimo { get; set; }
        public float Maximo { get; set; }
        public float MinimoCompra { get; set; }
        public int idCatalogoSat { get; set; }
        public string ImagenId { get; set; }
        public IList<ImpuestoProducto> Impuestos { get; set; }
        public IList<PLUProductos> PLUs { get; set; }
        public IList<ProductosProveedor> Proveedores { get; set; }

    }
}
