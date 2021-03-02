using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pos.Business.Model
{
    public class ProductosProveedor
    {
        public int idProductoProveedor { get; set; }
        [Required]
        public int idProveedor { get; set; }
        public Proveedores Proveedor { get; set; }
        [Required]
        public int idProducto { get; set; }
        public Productos Producto { get; set; }
        [Required]
        public float ultimoCosto { get; set; }
    }
}
