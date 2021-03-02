using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.Business.Model
{
    public class Retiro { 
        public long IdRetiro { get; set; }
        public long IdCorte { get; set; }
        public Cortes Corte { get; set; }
        public string Comentarios { get; set; }
        public DateTime Hora { get; set;}
        public double Cantidad { get; set; }
        public string Tipo { get; set; }
        public bool Estatus { get; set; }
    }
}
