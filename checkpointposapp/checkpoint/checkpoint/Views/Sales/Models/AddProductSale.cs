using checkpoint.Views.Catalogs.Products.Models;

namespace checkpoint.Sales.Models
{
    public class AddProductSale
    {
        public int idProducto { get; set; }
        public float cantidad { get; set; }
        public float monto { get; set; }
        public Productos Productos { get; set; }
    }
}
