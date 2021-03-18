using System;

namespace checkpoint.Views.withdrawCash.Models
{
    public class Retiros
    {
        public long IdRetiro { get; set; }
        public long IdCorte { get; set; }
        public string Hora { get; set; }
        public string Comentarios { get; set; }
        public double Cantidad { get; set; }
        public char Tipo { get; set; }
        public bool Estatus { get; set; }
    }
}
