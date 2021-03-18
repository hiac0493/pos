using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pos.Business.Model
{
    public class TipoUsuario
    {
        public TipoUsuario()
        {
            this.Usuarios = new List<Usuarios>();
        }
        public int idTipoUsuario { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public int NivelUsuario { get; set; }
        public virtual ICollection<Usuarios> Usuarios { get; set; }
    }
}
