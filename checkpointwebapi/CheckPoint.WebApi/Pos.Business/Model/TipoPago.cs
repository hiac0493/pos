using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pos.Business.Model
{
    public class TipoPago
    {
        public TipoPago()
        {
            this.Ventas = new List<VentaPagos>();
        }
        public int idTipoPago { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public bool Estatus { get; set; }
        public virtual ICollection<VentaPagos> Ventas { get; set; }
        public virtual ICollection<CortePagos> Cortes { get; set; }
    }
}
