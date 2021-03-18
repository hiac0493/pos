using checkpoint.Views.CancelSales.Models;
using System;
using System.Collections.Generic;

namespace checkpoint.CancelSales.Models
{
    public class Ventas
    {
        public long folioVenta { get; set; }
        public float subtotal { get; set; }
        public float impuestos { get; set; }
        public float total { get; set; }
        public DateTime fecha { get; set; }
        public float pagado { get; set; }
        public float cambio { get; set; }
        public float utilidad { get; set; }
        public int idUsuario { get; set; }
        public char estatus { get; set; }
        public int? idUsuarioCancela { get; set; }
        public ICollection<AddProductSale> productos { get; set; }
        public ICollection<VentaPagos> pagos { get; set; }
        public ICollection<Impuestos> impuesto { get; set; }
    }
}
