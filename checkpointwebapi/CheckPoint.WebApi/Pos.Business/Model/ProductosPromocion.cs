using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.Business.Model
{
    public class ProductosPromocion
    {
        public long idProductoPromocion { get; set; }
        public long idPromocion { get; set; }
        public virtual Promociones Promocion { get; set; }
        public int idProducto { get; set; }
        public float Cantidad  { get; set; }
        public virtual Productos Producto { get; set; }
    }
}
