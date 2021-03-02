using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Business.Model
{
    public class Pantallas
    {
        public int idPantalla { get; set; }
        public string NombrePantalla{ get; set; }
        public string TextoPanel { get; set; }
        public string Url { get; set; }
        public string Icono { get; set; }
        public bool Activo { get; set; }
        public bool SubPantalla { get; set; }
    }
}
