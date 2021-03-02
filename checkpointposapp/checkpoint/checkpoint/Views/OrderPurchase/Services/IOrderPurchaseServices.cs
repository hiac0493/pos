using checkpoint.OrderPurcharse.Models;
using checkpoint.Views.OrderPurcharse.Models;
using checkpoint.Views.OrderPurchase.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace checkpoint.OrderPurcharse.Services
{
    public interface IOrderPurchaseServices
    {
        ProductToBuy GetProductWithSuppliers(string pluProducto);
        Task<Compras> AddCompra(long orden, Compras compra);
        void AddOrdenes(List<Ordenes> ordenes);
        List<Ordenes> GetAllOrders();
        List<Ordenes> GetOrdersByEstatusWithSupplier(char estatusSearch);
        Task<IEnumerable<ProductToBuy>> GetProductsByList(List<ProductosCompra> productsList);
        IEnumerable<ProveedorEntity> GetSuppliersWithProducts();
        Task<ProveedorEntity> SaveProveedor(ProveedorEntity proveedor);
        IEnumerable<ProveedorEntity> GetSuppliersByName(string name);
        List<Departamentos> GetAllDepartamentos();
        IEnumerable<ProductDepartment> GetAllProductsByDepartment(int idDepartamento);
        void DeleteProductoProveedor(ProductosProveedor productoProveedor);
        void UpdateOrder(Ordenes orden);
        IEnumerable<ProductToBuy> GetProductsSuggestToBuy(int supplier);
    }
}
