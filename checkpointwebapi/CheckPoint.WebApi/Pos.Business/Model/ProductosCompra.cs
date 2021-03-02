using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pos.Business.Model
{
    public class ProductosCompra
    {
        public ProductosCompra()
        {
            this.Lotes = new List<VentaLote>();
        }
        public long idCompraProducto { get; set; }
        [Required]
        public long idCompra { get; set; }
        public virtual Compras Compra { get; set; }
        [Required]
        public int idProducto { get; set; }
        public virtual Productos Producto { get; set; }
        [Required]
        public float Cantidad { get; set; }
        [Required]
        public float Monto { get; set; }
        [Required]
        public float Restante { get; set; }
        [Required]
        public float Costo { get; set; }
        public virtual ICollection<VentaLote> Lotes { get; set; }
    }
}
