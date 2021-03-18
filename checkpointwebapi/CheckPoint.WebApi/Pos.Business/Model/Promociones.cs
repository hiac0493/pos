using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.Business.Model
{
    public class Promociones
    {
        public Promociones()
        {
            this.Productos = new List<ProductosPromocion>();
        }
        public long idPromocion { get; set; }
        public string NombrePromocion { get; set; }
        public long Inicio { get; set; }
        public long Fin { get; set; }
        public float? Monto { get; set; }
        public float? Porcentaje { get; set; }
        public int? idDepartamento { get; set; }
        public virtual Departamentos Departamento { get; set; }
        public int? idMarca { get; set; }
        public virtual Marca Marca { get; set; }
        public string DiasPromocion { get; set; }
        public bool Estatus { get; set; }
        public ICollection<ProductosPromocion> Productos { get; set; }
    }
}
