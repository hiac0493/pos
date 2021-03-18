using System;
using System.Collections.Generic;
using System.Text;

namespace checkpoint.Views.Catalogs.Permissions.Models
{
    public class PermissionsUserDto
    {
        public long idUsuario { get; set; }
        public IEnumerable<PantallasUsuario> pantallasUsuario { get; set; }
    }
}
