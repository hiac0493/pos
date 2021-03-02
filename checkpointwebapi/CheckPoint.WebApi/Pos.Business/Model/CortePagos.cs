using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.Business.Model
{
    public class CortePagos
    {
        public long IdCortePago { get; set; }
        public long IdCorte { get; set; }
        public Cortes Corte { get; set; }
        public int IdTipoPago { get; set; }
        public TipoPago TipoPago { get; set; }
        public float Cantidad { get; set; }
    }
}
