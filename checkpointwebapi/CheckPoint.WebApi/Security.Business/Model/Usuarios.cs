using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Business.Model
{
    public class Usuarios
    {
        public Usuarios()
        {
            this.PantallasUsuario = new List<PantallasUsuario>();
        }
        public int idUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoMaterno { get; set; }
        public string ApellidoPaterno { get; set; }
        public string Nombres { get; set; }
        public string Contraseña { get; set; }
        public bool Activo { get; set; }
        public int idTipoUsuario { get; set; }
        public ICollection<PantallasUsuario> PantallasUsuario { get; set; }
    }
}
