using checkpoint.Views.OrderPurcharse.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace checkpoint.OrderPurcharse.Models
{
    public class ProductToBuy
    {
        public int idProducto { get; set; }
        public float quantity { get; set; }
        public string description { get; set; }
        public int idProveedor { get; set; }
        public ProveedorDto[] supplier { get; set; }
        public List<Impuesto> impuestos { get; set; }
        public float price { get; set; }
        public float total { get; set; }
    }
}
