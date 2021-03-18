using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Business.Model
{
    public class PantallasUsuario
    {
        public long idPantallasUsuario { get; set; }
        public int idUsuario { get; set; }
        public int idPantalla { get; set; }
        public virtual Usuarios Usuario { get; set; }
        public virtual Pantallas Pantalla { get; set; }
    }
}
