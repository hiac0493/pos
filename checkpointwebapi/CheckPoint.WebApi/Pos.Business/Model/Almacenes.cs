using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pos.Business.Model
{
    public class Almacenes
    {
        [Required]
        public int IdAlmacen { get; set; }
        [Required]
        public string Nombre { get; set; }
        public int Existencia { get; set; }
        public string Observaciones { get; set; }
        public bool Activo { get; set; }

    }
}
