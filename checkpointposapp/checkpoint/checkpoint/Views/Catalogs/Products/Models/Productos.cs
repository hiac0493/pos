using System.Collections.Generic;

namespace checkpoint.Views.Catalogs.Products.Models
{
    public class Productos
    {
        public string PLU
        {
            get
            {
                return PLUs[0].PLU;
            }
        }
        public int idProducto { get; set; }
        public string NombreProducto { get; set; }
        public float PrecioCosto { get; set; }
        public float PrecioVenta { get; set; }
        public float PrecioVentaSinImpuestos { get; set; }
        public int idMarca { get; set; }
        public int idDepartamento { get; set; }
        public int idSubDepartamento { get; set; }
        public float Ganancia { get; set; }
        public int idUnidad { get; set; }
        public float Minimo { get; set; }
        public float Maximo { get; set; }
        public float MinimoCompra { get; set; }
        public int? idCatalogoSat { get; set; }
        public int? idUnidadSat { get; set; }
        public string ImagenId { get; set; }
        public IList<ImpuestoProducto> Impuestos { get; set; }
        public IList<PLUProductos> PLUs { get; set; }
        public IList<ProductosProveedor> Proveedores { get; set; }

    }
}
