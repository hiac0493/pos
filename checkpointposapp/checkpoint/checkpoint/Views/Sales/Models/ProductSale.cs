using checkpoint.Views.Sales.Models;
using System.Collections.Generic;

namespace checkpoint.Sales.Models
{
    public class ProductSale
    {
        public int idProducto { get; set; }
        public string pluProducto { get; set; }
        public string nombre { get; set; }
        public long existencia { get; set; }
        public float precioVenta { get; set; }
        public string imagenId { get; set; }
        public List<Impuestos> impuestos { get; set; }
        public List<Promociones> promociones { get; set; }
    }
}
