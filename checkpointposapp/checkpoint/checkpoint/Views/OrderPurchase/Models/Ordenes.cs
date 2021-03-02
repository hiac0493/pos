using checkpoint.Views.OrderPurcharse.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace checkpoint.Views.OrderPurchase.Models
{
    public class Ordenes
    {
        public long idOrden { get; set; }
        public EstatusOrders[] estatusOrders { get; set; }
        public char estatus { get; set; }
        public int idProveedor { get; set; }
        public string Proveedor { get; set; }
        public float NumeroArticulos { get; set; }
        public float Total { get; set; }
        public long? idCompra { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime UltimaModificacion { get; set; }
        public int? UsuarioAutoriza { get; set; }
        public DateTime? FechaAutorizacion { get; set; }
        public ICollection<ProductosCompra> ProductosOrden { get; set; }
    }
}
