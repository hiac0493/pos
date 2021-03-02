using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace checkpoint.Views.OrderPurchase.Models
{
    public class ProveedorEntity
    {
        public ProveedorEntity()
        {
            this.productosWithDepartment = new List<ProductDepartment>();
            this.productos = new List<ProductosProveedor>();
        }
        public int idProveedor { get; set; }
        public string nombre { get; set; }
        public string representante { get; set; }
        public string telefonos { get; set; }
        public string correos { get; set; }
        public string paginaWeb { get; set; }
        public string comentarios { get; set; }
        public bool estatus { get; set; }
        public IEnumerable<ProductDepartment> productosWithDepartment { get; set; } 
        public List<ProductosProveedor> productos { get; set; }
    }
}
