using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pos.Business.Model
{
    public class Ordenes
    {
        public Ordenes()
        {
            this.ProductosOrden = new List<ProductosOrden>();
        }
        public long idOrden { get; set; }
        [Required]
        public char Estatus { get; set; }
        [Required]
        public int idProveedor { get; set; }
        public Proveedores Proveedor { get; set; }
        [Required]
        public float NumeroArticulos { get; set; }
        [Required]
        public float Total { get; set; }
        public long? idCompra { get; set; }
        public Compras Compra { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
        [Required]
        public DateTime UltimaModificacion { get; set; }
        public int? UsuarioAutoriza { get; set; }
        public Usuarios Usuario { get; set; }
        public DateTime? FechaAutorizacion { get; set; }
        public ICollection<ProductosOrden> ProductosOrden { get; set; }
    }
}
