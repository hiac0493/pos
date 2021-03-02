using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pos.Business.Model
{
    public class UnidadSat
    {
        public UnidadSat()
        {
            this.UnidadesCatalogoSat = new List<CatalogoSat>();
        }

        public int idUnidadSat { get; set; }
        [Required]
        public string ClaveUnidad { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public ICollection<CatalogoSat> UnidadesCatalogoSat { get; set; }
    }
}
