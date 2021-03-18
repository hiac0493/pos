using System;
using System.Collections.Generic;
using System.Text;

namespace checkpoint.Views.Promotions.Models
{
    public class ProductosPromocion
    {
        public long idProductoPromocion { get; set; }
        public string plu { get; set; }
        public string Nombre { get; set; }
        public long idPromocion { get; set; }
        public int idProducto { get; set; }
        public float Cantidad { get; set; }
    }
}
