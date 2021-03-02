using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace checkpoint.Views.Catalogs.Products.Models
{
    public class ImageProduct
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int idProducto { get; set; }
        public Image Image { get; set; }
    }
}
