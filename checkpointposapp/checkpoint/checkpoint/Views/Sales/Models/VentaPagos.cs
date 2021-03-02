using System;
using System.Collections.Generic;
using System.Text;

namespace checkpoint.Sales.Models
{
    public class VentaPagos
    {
        public long idVentaPago { get; set; }
        public long idVenta { get; set; }
        public int idTipoPago { get; set; }
        public float cantidad { get; set; }
        public string referencia { get; set; }
    }
}
