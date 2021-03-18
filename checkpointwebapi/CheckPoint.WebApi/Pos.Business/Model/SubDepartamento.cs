using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.Business.Model
{
    public class SubDepartamento
    {
        public SubDepartamento()
        {
            this.Productos = new List<Productos>();
        }
        public int IdSubDepartamento { get; set; }
        public int IdDepartamento { get; set; }
        public Departamentos Departamento { get; set; }
        public string Nombre { get; set; }
        public ICollection<Productos> Productos { get; set; }
    }
}
