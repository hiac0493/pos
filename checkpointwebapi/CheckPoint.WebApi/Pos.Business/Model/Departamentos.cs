using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pos.Business.Model
{
    public class Departamentos
    {
        public Departamentos()
        {
            this.Productos = new List<Productos>();
            this.SubDepartamentos = new List<SubDepartamento>();
            this.Promociones = new List<Promociones>();
        }
        public int idDepartamento { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public bool Estatus { get; set; }
        public ICollection<Productos> Productos { get; set; }
        public ICollection<SubDepartamento> SubDepartamentos { get; set; }
        public ICollection<Promociones> Promociones { get; set; }
    }
}
