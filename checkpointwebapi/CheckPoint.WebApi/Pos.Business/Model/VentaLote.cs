using System;

namespace Pos.Business.Model
{
    public class VentaLote
    {
        public long idVentaLote { get; set; }
        public long idVenta { get; set; }
        public virtual Ventas venta { get; set; }
        public long idProductoCompra { get; set; }
        public virtual ProductosCompra compra { get; set; }
        public int idProducto { get; set; }
        public virtual Productos producto { get; set; }
        public float cantidad { get; set; }
        public bool estatus { get; set; }
    }
}
