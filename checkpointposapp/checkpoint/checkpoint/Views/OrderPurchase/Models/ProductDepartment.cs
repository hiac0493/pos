using System;
using System.Collections.Generic;
using System.Text;

namespace checkpoint.Views.OrderPurchase.Models
{
    public class ProductDepartment
    {
        public int idProveedor { get; set; }
        public int idProducto { get; set; }
        public string pluProducto { get; set; }
        public int idDepartamento { get; set; }
        public string descripcion { get; set; }
    }
}
