using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckPoint.WebApi.Dtos
{
    public class ProductoVenta
    {
        public int idProducto { get; set; }
        public string pluProducto { get; set; }
        public string nombre { get; set; }
        public float existencia { get; set;}
        public float precioVenta { get; set; }
    }
}
