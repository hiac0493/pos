using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pos.Business.Model
{
    public class Ventas
    {
        public Ventas()
        {
            this.Productos = new List<ProductosVenta>();
            this.Pagos = new List<VentaPagos>();
            this.Impuesto = new List<VentaImpuestos>();
            this.Lotes = new List<VentaLote>();
        }
        public long FolioVenta { get; set; }
        [Required]
        public float Subtotal { get; set; }
        [Required]
        public float Impuestos { get; set; }
        [Required]
        public float Total { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public float Pagado { get; set; }
        [Required]
        public float Cambio { get; set; }
        [Required]
        public float Utilidad { get; set; }
        [Required]
        public int idUsuario { get; set; }
        public Usuarios Usuario { get; set; }
        [Required]
        public char Estatus { get; set; }
        public int? idUsuarioCancela { get; set; }
        public Usuarios UsuarioCancela { get; set; }
        public virtual ICollection<ProductosVenta> Productos { get; set; }
        public virtual ICollection<VentaPagos> Pagos { get; set; }
        public virtual ICollection<VentaImpuestos> Impuesto { get; set; }
        public virtual ICollection<VentaLote> Lotes { get; set; }
    }
}
