using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Business.Model
{
    public class Usuarios
    {
        public int idUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoMaterno { get; set; }
        public string ApellidoPaterno { get; set; }
        public string Nombres { get; set; }
        public string Contraseña { get; set; }
        public bool Activo { get; set; }
    }
}
