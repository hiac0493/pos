using checkpoint.Views.Inventory.Models;
using System.Collections.Generic;

namespace checkpoint.Views.Inventory.Services
{
    public interface IInventoryServices
    {
        IEnumerable<Almacenes> GetAllAlmacenes();
        IEnumerable<ProductoAlmacenDto> GetProductosByAlmacen(int idAlmacen);
    }
}
