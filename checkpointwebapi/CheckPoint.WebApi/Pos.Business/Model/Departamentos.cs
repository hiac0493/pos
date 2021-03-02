using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pos.Business.Model
{
    public class Departamentos
    {
        public Departamentos()
        {
            this.Productos = new List<Productos>();
        }
        public int idDepartamento { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public bool Estatus { get; set; }
        public ICollection<Productos> Productos { get; set; }
    }
}
