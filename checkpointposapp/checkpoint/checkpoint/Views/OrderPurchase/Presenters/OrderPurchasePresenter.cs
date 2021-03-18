using checkpoint.OrderPurcharse.Models;
using checkpoint.OrderPurcharse.Services;
using checkpoint.Views.OrderPurcharse.Models;
using checkpoint.Views.OrderPurchase.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace checkpoint.OrderPurcharse.Presenters
{
    public class OrderPurchasePresenter
    {
        private readonly IOrderPurchaseServices _orderPurchaseServices;

        public OrderPurchasePresenter(IOrderPurchaseServices orderPurchaseServices)
        {
            _orderPurchaseServices = orderPurchaseServices;
        }

        public ProductToBuy GetProductWithSuppliers(string pluProducto)
        {
            return _orderPurchaseServices.GetProductWithSuppliers(pluProducto);
        }
        public Task<Compras> AddCompra(long orden, Compras compra)
        {
            return _orderPurchaseServices.AddCompra(orden, compra);
        }

        public void AddOrders(List<Ordenes> ordenes)
        {
            _orderPurchaseServices.AddOrdenes(ordenes);
        }

        public void UpdateOrder(Ordenes orden)
        {
            _orderPurchaseServices.UpdateOrder(orden);
        }

        public List<Ordenes> GetAllOrders()
        {
            return _orderPurchaseServices.GetAllOrders();
        }

        public List<Ordenes> GetOrdersByEstatusWithSupplier(char estatusSearch)
        {
            return _orderPurchaseServices.GetOrdersByEstatusWithSupplier(estatusSearch);
        }

        public IEnumerable<ProductToBuy> GetProductsByList(List<ProductosCompra> productsList)
        {
            return _orderPurchaseServices.GetProductsByList(productsList).Result;
        }

        public IEnumerable<ProveedorEntity> GetSuppliersWithProducts()
        {
            return _orderPurchaseServices.GetSuppliersWithProducts();
        }

        public Task<ProveedorEntity> SaveProveedor(ProveedorEntity proveedor)
        {
            return _orderPurchaseServices.SaveProveedor(proveedor);
        }

        public IEnumerable<ProveedorEntity> GetSuppliersByName(string name)
        {
            return _orderPurchaseServices.GetSuppliersByName(name);
        }

        public List<Departamentos> GetAllDepartamentos()
        {
            return _orderPurchaseServices.GetAllDepartamentos();
        }

        public IEnumerable<ProductDepartment> GetAllProductsByDepartment(int idDepartamento)
        {
            return _orderPurchaseServices.GetAllProductsByDepartment(idDepartamento);
        }

        public void DeleteProductoProveedor(ProductosProveedor productoProveedor)
        {
            _orderPurchaseServices.DeleteProductoProveedor(productoProveedor);
        }
        
        public IEnumerable<ProductToBuy> GetProductsSuggestToBuy(int supplier)
        {
            return _orderPurchaseServices.GetProductsSuggestToBuy(supplier);
        }

        public IEnumerable<Almacenes> GetAllAlmacenes()
        {
            return _orderPurchaseServices.GetAllAlmacenes();
        }
    }
}
