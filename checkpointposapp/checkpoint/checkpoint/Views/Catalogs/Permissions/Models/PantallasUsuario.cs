using System;
using System.Collections.Generic;
using System.Text;

namespace checkpoint.Views.Catalogs.Permissions.Models
{
    public class PantallasUsuario
    {
        public long idPantallasUsuario { get; set; }
        public int idUsuario { get; set; }
        public int idPantalla { get; set; }
        public Usuarios Usuario { get; set; }
        public Pantallas Pantalla { get; set; }
    }
}
