using System;
using System.Collections.Generic;
using System.Text;

namespace checkpoint.Views.Sales.Models
{
    public class Promociones
    {
        public long idPromocion { get; set; }
        public string NombrePromocion { get; set; }
        public long Inicio { get; set; }
        public long Fin { get; set; }
        public float? Monto { get; set; }
        public float? Porcentaje { get; set; }
        public int? idDepartamento { get; set; }
        public int? idMarca { get; set; }
        public string DiasPromocion { get; set; }
        public bool Estatus { get; set; }
    }
}
