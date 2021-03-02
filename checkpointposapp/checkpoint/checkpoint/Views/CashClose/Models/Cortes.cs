using Pos.Business.Model;
using System;

namespace checkpoint.Views.CashClose.Models
{
    public class Cortes
    {
        public long IdCorte { get; set; }
        public int IdTurno { get; set; }
        public int IdCaja { get; set; }
        public Cajas Caja { get; set; }
        public Turnos Turno { get; set; }
        public int IdUsuario { get; set; }
        public long FolioVentaInicio { get; set; }
        public long FolioVentaFin { get; set; }
        public double FondoCaja { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public double TotalVenta { get; set; }
        public double TotalUtilidad { get; set; }

    }
}
