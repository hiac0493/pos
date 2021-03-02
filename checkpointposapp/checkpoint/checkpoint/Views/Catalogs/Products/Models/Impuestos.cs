using System;
using System.ComponentModel;

namespace checkpoint.Views.Catalogs.Products.Models
{
    public class Impuestos
    {
        public int idImpuesto { get; set; }
        public string Descripcion { get; set; }
        public float Porcentaje { get; set; }

        public static implicit operator BindingList<object>(Impuestos v)
        {
            throw new NotImplementedException();
        }
    }
}
