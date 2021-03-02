using checkpoint.Views.CashClose.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pos.Business.Model
{
    public class Cajas
    {
        public int IdCaja  { get; set;}
        [Required]
        public string Nombre { get; set; }
        [Required]
        public bool Estatus { get; set; }
        public virtual ICollection<Cortes> Cortes { get; set; }
    }
}
