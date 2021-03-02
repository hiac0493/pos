using System;
using System.Collections.Generic;
using System.Text;

namespace checkpoint.CheckPrices.Models
{
    public class PLUProductCheck
    {
        public int idProducto { get; set; }
        public string pluProducto { get; set; }
        public string nombre { get; set; }
        public long existencia { get; set; }
        public long precioVenta { get; set; }
    }
}
