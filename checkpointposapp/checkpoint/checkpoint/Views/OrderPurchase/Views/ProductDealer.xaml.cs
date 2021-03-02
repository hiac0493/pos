using checkpoint.Helpers;
using checkpoint.OrderPurcharse.Presenters;
using checkpoint.Views.OrderPurchase.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace checkpoint.Views.OrderPurcharse.Views
{
    /// <summary>
    /// Interaction logic for ProductDealer.xaml
    /// </summary>
    public partial class ProductDealer : Window
    {
        private OrderPurchasePresenter _orderPurchasePresenter;
        BindingList<ProductDepartment> productsDepartment = new BindingList<ProductDepartment>();
        BindingList<ProductDepartment> productsSupplier = new BindingList<ProductDepartment>();
        ProveedorEntity _supplierToSave = new ProveedorEntity();

        public List<ProductosProveedor> ProductsSupplier { get { return _supplierToSave.productos; } }
        public IEnumerable<ProductDepartment> ProductsDepartment { get { return _supplierToSave.productosWithDepartment; } }

        public ProductDealer(OrderPurchasePresenter orderPurchasePresenter, string supplier, ProveedorEntity supplierToSave)
        {
            InitializeComponent();
            _supplierToSave = supplierToSave;
            _orderPurchasePresenter = orderPurchasePresenter;
            supplierNametxt.Text = supplier;
            List<Departamentos> departmentsList = new List<Departamentos>();
            departmentsList.Add(new Departamentos { idDepartamento = 0, Descripcion = "TODOS" });
            departmentsList.AddRange(_orderPurchasePresenter.GetAllDepartamentos());
            comboDepartmentsProducts.ItemsSource = departmentsList;
            productsDepartmentGrid.ItemsSource = productsDepartment;
            productsSupplierGrid.ItemsSource = productsSupplier;
            productsSupplier.AddRange(_supplierToSave.productosWithDepartment);
            comboDepartmentsProducts.SelectedValue = 0;
        }

        private void comboDepartmentsProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int idDepartment = (int)comboDepartmentsProducts.SelectedValue;
            productsSupplier.Clear();
            if (idDepartment != 0)
                productsSupplier.AddRange(_supplierToSave.productosWithDepartment.Where(x => x.idDepartamento.Equals(idDepartment)));
            else
                productsSupplier.AddRange(_supplierToSave.productosWithDepartment);
            productsDepartment.Clear();
            productsDepartment.AddRange(_orderPurchasePresenter.GetAllProductsByDepartment(idDepartment).Where(x => !_supplierToSave.productosWithDepartment.Select(y => y.idProducto).ToList().Contains(x.idProducto)));
        }


        private void assignBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ProductDepartment currentProduct = productsDepartmentGrid.SelectedItem as ProductDepartment;
            if (currentProduct != null)
            {
                productsDepartment.Remove(currentProduct);
                productsSupplier.Add(currentProduct);
                ProductosProveedor prodSupplierToAdd = new ProductosProveedor { idProducto = currentProduct.idProducto, idProveedor = _supplierToSave.idProveedor, ultimoCosto = 0 };
                _supplierToSave.productos.Add(prodSupplierToAdd);
                List<ProductDepartment> listProductsSupplier = productsSupplier.ToList();
                listProductsSupplier.AddRange(_supplierToSave.productosWithDepartment);
                _supplierToSave.productosWithDepartment = listProductsSupplier.ToList();
            }
        }

        private void deleteProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductDepartment currentProduct = ((Button)(sender)).DataContext as ProductDepartment;
            if (currentProduct != null)
            {
                productsSupplier.Remove(currentProduct);
                productsDepartment.Add(currentProduct);
                ProductosProveedor prodSupplierToRemove = (from producto in _supplierToSave.productos where producto.idProducto.Equals(currentProduct.idProducto) select producto).FirstOrDefault();
                if (prodSupplierToRemove != null && prodSupplierToRemove.idProductoProveedor != 0)
                    _orderPurchasePresenter.DeleteProductoProveedor(prodSupplierToRemove);
                _supplierToSave.productos.Remove(prodSupplierToRemove);
                _supplierToSave.productosWithDepartment = productsSupplier.ToList();
            }
        }
    }
}
 