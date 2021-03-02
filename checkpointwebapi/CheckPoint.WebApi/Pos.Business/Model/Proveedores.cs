using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace Pos.Business.Model
{
    public class Proveedores
    {
        public Proveedores()
        {
            this.Productos = new List<ProductosProveedor>();
            this.Ordenes = new List<Ordenes>();
        }
        public int idProveedor { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string Representante { get; set; }
        public string Telefonos { get; set; }
        public string Correos { get; set; }
        public string PaginaWeb { get; set; }
        public string Comentarios { get; set; }
        public bool Estatus { get; set; }
        public virtual ICollection<ProductosProveedor> Productos { get; set; }
        public virtual ICollection<Ordenes> Ordenes { get; set; }
    }
}
