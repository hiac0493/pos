using System;
using System.Collections.Generic;
using System.Text;

namespace checkpoint.Views.OrderPurcharse.Models
{
    public class Compras
    {
        public long FolioCompra { get; set; }
        public float Subtotal { get; set; }
        public float Impuestos { get; set; }
        public float Total { get; set; }
        public int idUsuario { get; set; }
        public char Estatus { get; set; }
        public int? idUsuarioCancela { get; set; }
        public int idAlmacen { get; set; }
        public DateTime Fecha { get; set; }
        public ICollection<ProductosCompra> ProductosCompra { get; set; }
    }
}
