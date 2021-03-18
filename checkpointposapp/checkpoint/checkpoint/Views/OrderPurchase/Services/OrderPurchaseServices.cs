using checkpoint.OrderPurcharse.Models;
using checkpoint.Resources;
using checkpoint.Views.OrderPurcharse.Models;
using checkpoint.Views.OrderPurchase.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace checkpoint.OrderPurcharse.Services
{
    public class OrderPurchaseServices : IOrderPurchaseServices
    {
        public async Task<Compras> AddCompra(long orden, Compras compra)
        {
            string webApiUrl = WebApiMethods.AddCompra + orden;
            Compras compraResult = await App.HttpTools.HttpPostObjectWithResponseDataAsync<Compras, Compras>(webApiUrl, compra, "Hubo un error en el guardado de la venta").ConfigureAwait(false);
            return compraResult;
        }

        public void AddOrdenes(List<Ordenes> ordenes)
        {
            string webApiUrl = WebApiMethods.AddOrders;
            var result = App.HttpTools.HttpPostAsync(webApiUrl, ordenes);
        }

        public List<Departamentos> GetAllDepartamentos()
        {
            string webApiUrl = WebApiMethods.GetAllDepartamentos;
            IList<Departamentos> departmentsResult = new List<Departamentos>();
            var result = App.HttpTools.HttpGetList<Departamentos>(webApiUrl, ref departmentsResult, "Hubo un error en la lectura de los departamentos");
            if (result == HttpStatusCode.OK)
                return departmentsResult.ToList();
            else
                return null;
        }
         
        public List<Ordenes> GetAllOrders()
        {
            string webApiUrl = WebApiMethods.GetAllOrders;
            IList<Ordenes> ordersResult = new List<Ordenes>();
            var result = App.HttpTools.HttpGetList<Ordenes>(webApiUrl, ref ordersResult, "Error al leer la lista de ordenes");
            if (result == HttpStatusCode.OK)
                return ordersResult.ToList();
            else
                return null;
        }

        public IEnumerable<ProductDepartment> GetAllProductsByDepartment(int idDepartamento)
        {
            string webApiUrl = WebApiMethods.GetAllProductsByDepartment + idDepartamento;
            IList<ProductDepartment> productsResult = new List<ProductDepartment>();
            var result = App.HttpTools.HttpGetList<ProductDepartment>(webApiUrl, ref productsResult, "Hubo un error en la lectura de los productos");
            if (result == HttpStatusCode.OK)
                return productsResult;
            else
                return null;
        }

        public IEnumerable<ProveedorEntity> GetSuppliersWithProducts()
        {
            string webApiUrl = WebApiMethods.GetSuppliersWithProducts;
            IList<ProveedorEntity> suppliersList = new List<ProveedorEntity>();
            var result = App.HttpTools.HttpGetList<ProveedorEntity>(webApiUrl, ref suppliersList, "Error en la lectura de los proveedores");
            if (result == HttpStatusCode.OK)
                return suppliersList.ToList();
            else
                return null;
        }

        public async Task<IEnumerable<ProductToBuy>> GetProductsByList(List<ProductosCompra> productsList)
        {
            string webApiUrl = WebApiMethods.GetProductsByList;
            IEnumerable<ProductToBuy> productsResult = new List<ProductToBuy>();
            productsResult = await App.HttpTools.HttpPostObjectWithResponseDataAsync<ProductosCompra, ProductToBuy>(webApiUrl, productsList, productsResult, "Hubo un error en la lectura de los productos").ConfigureAwait(false);
            if (productsResult != null)
                return productsResult;
            else
                return null;
        }

        public ProductToBuy GetProductWithSuppliers(string pluProducto)
        {
            string webApiUrl = WebApiMethods.GetProductoWithSuppliers + pluProducto;
            ProductToBuy productToBuy = new ProductToBuy();
            HttpStatusCode resultProduct = App.HttpTools.HttpGetSingle<ProductToBuy>(webApiUrl, ref productToBuy, $"Error en la obteción de información del producto {pluProducto}");
            if(resultProduct == HttpStatusCode.OK)
                return productToBuy;
            else
                return null;
        }

        public IEnumerable<ProveedorEntity> GetSuppliersByName(string name)
        {
            string webApiUrl = WebApiMethods.GetSuppliersByName + name;
            IList<ProveedorEntity> suppliersListResult = new List<ProveedorEntity>();
            HttpStatusCode resultSuppliers = App.HttpTools.HttpGetList<ProveedorEntity>(webApiUrl, ref suppliersListResult, "Hubo un error en la lectura de los proveedores");
            if(resultSuppliers == HttpStatusCode.OK)
                return suppliersListResult != null ? suppliersListResult.ToList() : null;
            else
                return null;
        }

        public async Task<ProveedorEntity> SaveProveedor(ProveedorEntity proveedor)
        {
            string webApiUrl = WebApiMethods.SaveProveedor;
            ProveedorEntity supplierResult = await App.HttpTools.HttpPostObjectWithResponseDataAsync<ProveedorEntity, ProveedorEntity>(webApiUrl, proveedor, "Hubo un error el guardar el proveedor").ConfigureAwait(false);
            return supplierResult;
        }

        public void DeleteProductoProveedor(ProductosProveedor productoProveedor)
        {
            string webApiUrl = WebApiMethods.DeleteProductoProveedor;
            var result = App.HttpTools.HttpDeleteAsync(webApiUrl, productoProveedor, "Hubo un error en la eliminación del producto");
        }

        public void UpdateOrder(Ordenes orden)
        {
            string webApiUrl = WebApiMethods.UpdateOrder;
            var result = App.HttpTools.HttpPostAsync(webApiUrl, orden);
        }

        public IEnumerable<ProductToBuy> GetProductsSuggestToBuy(int supplier)
        {
            string webApiUrl = WebApiMethods.GetProductsSuggestToBuy + supplier;
            IList<ProductToBuy> productsToBuySuggestResult = new List<ProductToBuy>();
            HttpStatusCode result = App.HttpTools.HttpGetList<ProductToBuy>(webApiUrl, ref productsToBuySuggestResult, "Hubo un error en la lectura de los productos para compra");
            if (result == HttpStatusCode.OK)
                return productsToBuySuggestResult;
            else
                return null;
        }

        public List<Ordenes> GetOrdersByEstatusWithSupplier(char estatusSearch)
        {
            string webApiUrl = WebApiMethods.GetOrdersByEstatusWithSupplier + estatusSearch;
            IList<Ordenes> ordersResult = new List<Ordenes>();
            var result = App.HttpTools.HttpGetList<Ordenes>(webApiUrl, ref ordersResult, "Error al leer la lista de ordenes");
            if (result == HttpStatusCode.OK)
                return ordersResult.ToList();
            else
                return null;
        }

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
    }
}

