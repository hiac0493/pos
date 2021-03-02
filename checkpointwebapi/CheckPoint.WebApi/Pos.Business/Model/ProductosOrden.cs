using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pos.Business.Model
{
    public class ProductosOrden
    {
        public long idProductoOrden { get; set; }
        [Required]
        public long idOrden { get; set; }
        public Ordenes Orden { get; set; }
        [Required]
        public int idProducto { get; set; }
        public Productos Producto { get; set; }
        [Required]
        public float Cantidad { get; set; }
        [Required]
        public float Monto { get; set; }
    }
}
