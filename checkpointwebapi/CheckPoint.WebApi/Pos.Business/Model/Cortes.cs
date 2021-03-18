using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pos.Business.Model
{
    public class Cortes
    {
        public Cortes()
        {
            this.Pagos = new List<CortePagos>();
            this.Retiros = new List<Retiro>();
        }
        public long IdCorte { get; set; } 
        public int IdTurno { get; set; }
        public Turnos Turno { get; set; }    
        public int IdCaja { get; set; }
        public Cajas Caja { get; set; }
        public int IdUsuario { get; set; }
        public Usuarios Usuario { get; set; }
        public long FolioVentaInicio { get; set; }
        public long? FolioVentaFin { get; set; }
        public double FondoCaja { get; set; }
        public DateTime FechaInicio { get; set; }

        public DateTime FechaFinal { get; set; }

        public double TotalVenta { get; set; }

        public double TotalUtilidad { get; set; }
        public virtual ICollection <CortePagos> Pagos { get; set; }
        public virtual ICollection<Retiro> Retiros { get; set; }
    }
}
