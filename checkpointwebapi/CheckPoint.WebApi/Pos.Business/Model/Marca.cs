using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pos.Business.Model
{
    public class Marca
    {
        public Marca()
        {
            this.Productos = new List<Productos>();
            this.Promociones = new List<Promociones>();
        }
        public int idMarca { get; set; }
        [Required]
        public string Descripcion { get; set; }

        public bool Activo { get; set; }

        public ICollection<Productos> Productos { get; set; }
        public ICollection<Promociones> Promociones { get; set; }
    }
}
