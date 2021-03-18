using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pos.Business.Model
{
    public class Pantallas
    {
        public Pantallas()
        {
            this.PantallasUsuarios = new List<PantallasUsuario>();
        }
        public int idPantalla { get; set; }
        [Required]
        public string NombrePantalla { get; set; }
        [Required]
        public string TextoPanel { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public string Icono { get; set; }
        [Required]
        public int Nivel { get; set; }
        [Required]
        public bool Activo { get; set; }
        [Required]
        public bool SubPantalla { get; set; }
        [Required]
        public ICollection<PantallasUsuario> PantallasUsuarios { get; set; }
    }
}
