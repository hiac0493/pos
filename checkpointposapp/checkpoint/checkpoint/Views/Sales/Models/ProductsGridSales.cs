using checkpoint.Views.Sales.Models;
using System.Collections.Generic;

namespace checkpoint.Sales.Models
{
    public class ProductsGridSales
    {
        public int idProducto { get; set; }
        public string PLU { get; set; }
        public float Quantity { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public float Total { get; set; }
        public List<Impuestos> impuestosList { get; set; }
    }
}
