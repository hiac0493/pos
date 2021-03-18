using Pos.Business.Model;
using System.Collections.Generic;

namespace CheckPoint.WebApi.Dtos
{
    public class PermissionsUserDto
    {
        public long idUsuario { get; set; }
        public ICollection<PantallasUsuario> pantallasUsuario { get; set; }
    }
}
