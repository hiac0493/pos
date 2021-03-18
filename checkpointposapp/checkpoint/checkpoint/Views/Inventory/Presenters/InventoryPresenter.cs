using checkpoint.Views.Inventory.Models;
using checkpoint.Views.Inventory.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace checkpoint.Views.Inventory.Presenters
{
    public class InventoryPresenter
    {
        private readonly IInventoryServices _inventoryServices; 
        public InventoryPresenter(IInventoryServices inventoryServices)
        {
            _inventoryServices = inventoryServices;
        }

        public IEnumerable<Almacenes> GetAllAlmacenes()
        {
            return _inventoryServices.GetAllAlmacenes();
        }
        public IEnumerable<ProductoAlmacenDto> GetProductosByAlmacen(int idAlmacen)
        {
            return _inventoryServices.GetProductosByAlmacen(idAlmacen);
        }

    }
}
