using System;
using System.Collections.Generic;
using System.Text;

namespace checkpoint.Views.Inventory.Models
{
    public class Almacenes
    {
        public int idAlmacen { get; set; }
        public string Nombre { get; set; }
        public string Observaciones { get; set; }
        public bool Principal { get; set; }
        public bool Activo { get; set; }
    }
}
