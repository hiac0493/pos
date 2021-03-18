using System;
using System.Collections.Generic;
using System.Text;

namespace checkpoint.Views.CancelSales.Models
{
    public class Impuestos
    {
        public int idImpuesto { get; set; }
        public string descripcion { get; set; }
        public float porcentaje { get; set; }
        public float cantidad { get; set; }
    }
}
