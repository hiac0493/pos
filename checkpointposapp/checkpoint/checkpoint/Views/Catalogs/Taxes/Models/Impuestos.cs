using System;
using System.Collections.Generic;
using System.Text;

namespace checkpoint.Views.Catalogs.Taxes.Models
{
    public class Impuestos
    {
        public int idImpuesto { get; set; }
        public string Descripcion { get; set; }
        public float Porcentaje { get; set; }
        public bool Estatus { get; set; }
    }
}
