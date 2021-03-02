using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pos.Business.Model
{
    public class VentaImpuestos
    {
        [Required]
        public long IdVentaImpuesto { get; set; }
        [Required]
        public long IdVenta { get; set; }
        public Ventas Venta { get; set; }
        [Required]
        public int IdImpuesto { get; set; }
        public Impuestos Impuesto { get; set; }
        [Required]
        public double Cantidad { get; set; }
    }
}
