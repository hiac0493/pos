using System;
using System.Collections.Generic;
using System.Text;

namespace checkpoint.Views.Catalogs.Permissions.Models
{
    public class Usuarios
    {
        public int idUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoMaterno { get; set; }
        public string ApellidoPaterno { get; set; }
        public string Nombres { get; set; }
        public string Nombre { get { return $"{Nombres} {ApellidoPaterno} {ApellidoMaterno}"; } }
        public List<PantallasUsuario> PantallasUsuario { get; set; }
    }
}
