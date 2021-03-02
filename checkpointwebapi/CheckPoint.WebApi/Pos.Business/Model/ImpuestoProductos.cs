using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pos.Business.Model
{
    public class ImpuestoProductos
    {
        public int idImpuestoProducto { get; set; }
        [Required]
        public int idImpuesto { get; set; }
        public Impuestos Impuesto { get; set; }
        [Required]
        public int idProducto { get; set; }
        public Productos Producto { get; set; }
    }
}
