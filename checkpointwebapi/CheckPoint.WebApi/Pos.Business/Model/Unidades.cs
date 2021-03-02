using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pos.Business.Model
{
    public class Unidades
    {
        public Unidades()
        {
            this.Productos = new List<Productos>();
        }

        public int idUnidad { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public ICollection<Productos> Productos { get; set; }
    }
}
