using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pos.Business.Model
{
    public class Compras
    {
        public Compras()
        {
            this.ProductosCompra = new List<ProductosCompra>();
            this.OrdenesCompra = new List<Ordenes>();
        }
        public long FolioCompra { get; set; }
        [Required]
        public float Subtotal { get; set; }
        [Required]
        public float Impuestos { get; set;}
        [Required]
        public float Total { get; set; }
        [Required]
        public int idUsuario { get; set;}
        public virtual Usuarios Usuario { get; set; }
        [Required]
        public char Estatus { get; set; }
        public int? idUsuarioCancela { get; set;}
        public virtual Usuarios UsuarioCancela { get; set; }
        public int idAlmacen { get; set; }
        public virtual Almacenes Almacen { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        public ICollection<ProductosCompra> ProductosCompra { get; set; }
        public ICollection<Ordenes> OrdenesCompra { get; set; }
    }
}
