using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pos.Business.Model
{
    public class PantallasUsuario
    {

        [Required]
        public long idPantallasUsuario { get; set; }
        [Required]
        public int idUsuario { get; set; }
        [Required]
        public int idPantalla { get; set; }
        public virtual Usuarios Usuario { get; set; }
        public virtual Pantallas Pantalla { get; set; }
    }
}
