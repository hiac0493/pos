using checkpoint.Views.Catalogs.Products.Models;

namespace checkpoint.CancelSales.Models
{
    public class AddProductSale
    {
        public int idProducto { get; set; }
        public float cantidad { get; set; }
        public float monto { get; set; }
        public bool estatus { get;set; }
        public Productos Productos { get; set; }
    }
}
