﻿using System;
using System.Collections.Generic;
using System.Text;

namespace checkpoint.Views.Catalogs.Products.Models
{
    public class ProductosProveedor
    {
        public int idProductoProveedor { get; set; }
        public int idProveedor { get; set; }
        public int idProducto { get; set; }
        public float ultimoCosto { get; set; }
        public Proveedores proveedor { get; set; }
    }
}
