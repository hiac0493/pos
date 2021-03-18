using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.Business.Model
{
    public class ProductoAlmacen
    {
        public int idProductoAlmacen { get; set; }
        public int idAlmacen { get; set; }
        public virtual Almacenes Almacen { get; set; }
        public int idProducto { get; set; }
        public virtual Productos Producto { get; set; }
        public float Existencia { get; set; }
    }
}
