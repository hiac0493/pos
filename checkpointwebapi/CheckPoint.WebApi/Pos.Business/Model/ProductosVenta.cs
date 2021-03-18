using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pos.Business.Model
{
    public class ProductosVenta
    {
        public long idVentaProducto { get; set; }
        [Required]
        public long idVenta { get; set; }
        public Ventas Venta { get; set; }
        [Required]
        public int idProducto { get; set; }
        public Productos Productos { get; set; }
        [Required]
        public float Cantidad { get; set; }
        [Required]
        public float Monto { get; set; }
        public bool Estatus { get; set; }
    }
}
