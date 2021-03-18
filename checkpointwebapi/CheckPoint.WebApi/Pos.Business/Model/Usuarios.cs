using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pos.Business.Model
{
    public class Usuarios
    {
        public Usuarios()
        {
            this.VentasUsuario = new List<Ventas>();
            this.PantallasUsuario = new List<PantallasUsuario>();
            this.VentasCanceladas = new List<Ventas>();
            this.ComprasUsuario = new List<Compras>();
            this.ComprasCanceladas = new List<Compras>();
        }
        public int idUsuario { get; set; }
        [Required]
        public string NombreUsuario { get; set; }
        [Required]
        public string ApellidoMaterno { get; set; }
        [Required]
        public string ApellidoPaterno { get; set; }
        [Required]
        public string Nombres { get; set; }
        [Required]
        public string Contraseña { get; set; }
        [Required]
        public bool Activo { get; set; }
        [Required]
        public int idTipoUsuario { get; set; }
        public virtual TipoUsuario TipoUsuario { get; set; }
        public ICollection<Ventas> VentasUsuario { get; set; }
        public ICollection<PantallasUsuario> PantallasUsuario { get; set; }
        public ICollection<Ventas> VentasCanceladas { get; set; }
        public ICollection<Compras> ComprasUsuario { get; set; }
        public ICollection<Compras> ComprasCanceladas { get; set; }
        public ICollection<Ordenes> OrdenesAutorizadas { get; set; }
        public ICollection<Cortes> CortesUsuario { get; set; }
    }
}
