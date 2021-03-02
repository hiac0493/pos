using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pos.Business.Model
{
    public class CatalogoSat
    {
        public int idCatalogoSat { get; set; }
        [Required]
        public int idUnidadSat { get; set; }
        public UnidadSat unidadSat { get; set; }
        [Required]
        public string Clave { get; set; }
        [Required]
        public string Descripcion { get; set; }

        public bool Activo { get; set; }
        public ICollection<Productos> Productos { get; set; }
    }
}
