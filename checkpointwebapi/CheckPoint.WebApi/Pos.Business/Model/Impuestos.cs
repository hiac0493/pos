using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pos.Business.Model
{
    public class Impuestos
    {
        public Impuestos()
        {
            this.Productos = new List<ImpuestoProductos>();
            this.Venta = new List<VentaImpuestos>();
        }
        public int idImpuesto { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public float Porcentaje { get; set; }
        [Required]
        public bool Estatus { get; set; }
        public virtual ICollection<ImpuestoProductos> Productos { get; set; }
        public virtual ICollection<VentaImpuestos> Venta { get; set; }
    }
}
