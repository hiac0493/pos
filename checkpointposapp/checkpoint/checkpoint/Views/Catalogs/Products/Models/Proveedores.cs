using System;
using System.Collections.Generic;
using System.Text;

namespace checkpoint.Views.Catalogs.Products.Models
{
    public class Proveedores
    {
        public int idProveedor { get; set; }
        public string Nombre { get; set; }
        public List<ProductosProveedor> Productos { get; set; }
    }
}
