namespace checkpoint.Views.Catalogs.Products.Models
{
    public class ImpuestoProducto
    {
        public int idImpuestoProducto { get; set; }
        public int idImpuesto { get; set; }
        public int idProducto { get; set; }
        public Impuestos impuesto { get; set; }
    }
}
