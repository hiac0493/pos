﻿namespace checkpoint.Users.Models
{
    public class UsuariosRH
    {
        public int idUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string apellidoMaterno { get; set; }
        public string apellidoPaterno { get; set; }
        public string nombres { get; set; }
        public string contraseña { get; set; }
        public bool activo { get; set; }
    }
}
