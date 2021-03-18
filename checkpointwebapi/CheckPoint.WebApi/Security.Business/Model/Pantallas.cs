using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Business.Model
{
    public class Pantallas
    {
        public Pantallas()
        {
            this.PantallasUsuarios = new List<PantallasUsuario>();
        }
        public int idPantalla { get; set; }
        public string NombrePantalla { get; set; }
        public string TextoPanel { get; set; }
        public string Url { get; set; }
        public string Icono { get; set; }
        public int Nivel { get; set; }
        public bool Activo { get; set; }
        public bool SubPantalla { get; set; }
        public ICollection<PantallasUsuario> PantallasUsuarios { get; set; }
    }
}
