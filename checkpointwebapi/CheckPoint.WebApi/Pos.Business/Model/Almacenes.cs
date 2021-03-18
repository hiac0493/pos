using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pos.Business.Model
{
    public class Almacenes
    {
        public Almacenes()
        {
            this.Productos = new List<ProductoAlmacen>();
            this.Compras = new List<Compras>();
        }
        [Required]
        public int idAlmacen { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string Observaciones { get; set; }
        public bool Principal { get; set; }
        public bool Activo { get; set; }
        public virtual ICollection<ProductoAlmacen> Productos { get; set; }
        public virtual ICollection<Compras> Compras { get; set; }
    }
}
