using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pos.Business.Model
{
    public class VentaPagos
    {
        public long idVentaPago { get; set; }
        [Required]
        public long idVenta { get; set; }
        public Ventas Venta { get; set; }
        [Required]
        public int idTipoPago { get; set; }
        public TipoPago TipoPago { get; set; }
        [Required]
        public float Cantidad { get; set; }
        public string Referencia { get; set; }
    }
}
