using checkpoint.Resources;
using checkpoint.Views.Inventory.Models;
using System.Collections.Generic;
using System.Net;

namespace checkpoint.Views.Inventory.Services
{
    public class InventoryServices : IInventoryServices
    {
        public IEnumerable<Almacenes> GetAllAlmacenes()
        {
            string webApiUrl = WebApiMethods.GetAllAlmacenes;
            IList<Almacenes> almacenesResult = new List<Almacenes>();
            var result = App.HttpTools.HttpGetList<Almacenes>(webApiUrl, ref almacenesResult, "Error al obtener la lista de almacenes");
            if (result == HttpStatusCode.OK)
                return almacenesResult;
            else
                return null;
        }        
        public IEnumerable<ProductoAlmacenDto> GetProductosByAlmacen(int idAlmacen)
        {
            string webApiUrl = WebApiMethods.GetProductosByAlmacen + idAlmacen;
            IList<ProductoAlmacenDto> productoAlmacenDtos = new List<ProductoAlmacenDto>();
            var result = App.HttpTools.HttpGetList<ProductoAlmacenDto>(webApiUrl, ref productoAlmacenDtos, "Error al obtener los productos por almacen");
            if (result == HttpStatusCode.OK)
                return productoAlmacenDtos;
            else
                return null;
        }
    }
}
