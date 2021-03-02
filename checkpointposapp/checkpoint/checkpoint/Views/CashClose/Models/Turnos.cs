using checkpoint.Views.CashClose.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pos.Business.Model
{
    public class Turnos
    {
        public int IdTurno { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public bool Estatus { get; set; }
        public TimeSpan HoraInicio { get; set; }
        [Required]
        public TimeSpan HoraFin { get; set; }
        public virtual ICollection<Cortes> Cortes { get; set; }
    }
}
