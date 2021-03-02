using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pos.Business.Model
{
    public class PLUProductos
    {
        public int idPLU { get; set; }
        [Required]
        public int idProducto { get; set; }
        [Required]
        public string PLU { get; set; }
        public virtual Productos Producto { get; set; }
    }
}
