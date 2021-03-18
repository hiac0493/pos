using checkpoint.Helpers;
using checkpoint.OrderPurcharse.Models;
using checkpoint.OrderPurcharse.Presenters;
using checkpoint.OrderPurcharse.Services;
using checkpoint.Views.OrderPurcharse.Models;
using checkpoint.Views.OrderPurcharse.Views;
using checkpoint.Views.OrderPurchase.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace checkpoint.Views.OrderPurchase.Views
{
    /// <summary>
    /// Interaction logic for MainOrders.xaml
    /// </summary>
    public partial class MainOrders : UserControl
    {
        #region Properties
        //**************************************************
        //*             PROPERTIES
        //**************************************************
        private OrderPurchasePresenter _orderPurchasePresenter;
        BindingList<ProductToBuy> productsToBuyList = new BindingList<ProductToBuy>();
        BindingList<ProveedorEntity> suppliersList = new BindingList<ProveedorEntity>();
        ProveedorEntity supplierToSave = new ProveedorEntity();
        BindingList<Ordenes> ordersList = new BindingList<Ordenes>();
        bool cancelOrder = false;
        long idCurrentOrder = 0;
        float subtotal = 0;
        float impuestos = 0;
        float total = 0;
        #endregion

        #region Constructor
        //**************************************************
        //*             CONSTRUCTOR
        //**************************************************
        public MainOrders()
        {
            InitializeComponent();
            InitializeFormWithData();
        }
        #endregion

        #region Fill data on start
        private void InitializeFormWithData()
        {
            _orderPurchasePresenter = new OrderPurchasePresenter(new OrderPurchaseServices());
            productsToBuyList.ListChanged += ProductsToBuyList_ListChanged;
            ordersList.AddRange(_orderPurchasePresenter.GetAllOrders());
            IEnumerable<ProveedorEntity> proveedores = _orderPurchasePresenter.GetSuppliersWithProducts();
            IEnumerable<Almacenes> almacenes = _orderPurchasePresenter.GetAllAlmacenes();
            suppliersList.AddRange(proveedores);
            suppliersGrid.ItemsSource = suppliersList;
            ComboWarehouse.ItemsSource = almacenes.ToList();
            ComboWarehouse.SelectedIndex = 0;
            comboSuppliersGral.ItemsSource = proveedores.ToList();
            Order.ItemsSource = productsToBuyList;
            OrdersHome.ItemsSource = ordersList;
            initialDateSearch.SelectedDate = finalDateSearch.SelectedDate = DateTime.Now;
            checkTodas.IsChecked = true;
        }
        #endregion
         
        #region Write data
        private void addProductToBuy(ProductToBuy productResult, float quantity)
        {
            ProductToBuy productExist = productsToBuyList.Where(x => x.idProducto.Equals(productResult.idProducto)).FirstOrDefault();
            if (productExist != null)
            {
                productExist.quantity += quantity;
                productExist.total = productExist.price * productExist.quantity;
                productsToBuyList.ResetBindings();
            }
            else
            {
                productResult.quantity = quantity;
                productResult.total = productResult.price * quantity;
                productsToBuyList.Add(productResult);
            }
            textPago.Text = string.Empty;
            textPago.Focus();
        }
          
        private void updateProductToBuy(object sender)
        {
            ProductToBuy productSelected = Order.SelectedValue as ProductToBuy;
            productSelected.quantity = float.Parse((((TextBox)(sender)).Text));
            productSelected.total = productSelected.quantity * productSelected.price;
            productsToBuyList.ResetBindings();   
            textPago.Text = string.Empty;
            textPago.Focus();
        }

        private void updateProductPrice(object sender)
        {
            ProductToBuy productSelected = Order.SelectedValue as ProductToBuy;
            productSelected.price = float.Parse((((TextBox)(sender)).Text));
            productSelected.total = productSelected.quantity * productSelected.price;
            productsToBuyList.ResetBindings();
            textPago.Text = string.Empty;
            textPago.Focus();
        }

        private void generateOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if(cancelOrder)
            {
                productsToBuyList.Clear();
                cancelOrder = false;
                idCurrentOrder = 0;
                generateOrderButton.Content = "GENERAR \nORDEN(ES)";
            }
            else
            {
                List<int> proveedores = (from producto in productsToBuyList select producto.idProveedor).Distinct().ToList();
                List<ProductToBuy> withOutSupplier = (from producto in productsToBuyList where producto.idProveedor.Equals(0) select producto).ToList();
                if (withOutSupplier.Count > 0 && productsToBuyList.Count > 0)
                {
                    if (MessageBox.Show("Existen productos sin proveedor asignado, los cuales no pueden ser considerados para una orden, desea continuar?", "Alerta", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        saveOrders(proveedores);
                    }
                }
                else
                {
                    saveOrders(proveedores);
                }
            }
        }

        private void saveOrders(List<int> proveedores)
        { 
            List<Ordenes> ordersBySupplier = new List<Ordenes>();
            foreach (int proveedor in proveedores)
            {
                List<ProductosCompra> productsSupplier = (from producto in productsToBuyList where producto.idProveedor.Equals(proveedor) select new ProductosCompra { idProducto = producto.idProducto, Cantidad = producto.quantity, Monto = producto.total }).ToList();
                Ordenes currentOrder = new Ordenes
                {
                    estatus = 'E',
                    idProveedor = proveedor,
                    NumeroArticulos = productsSupplier.Sum(x => x.Cantidad),
                    Total = productsToBuyList.Where(x => x.idProveedor.Equals(proveedor)).Sum(x => x.total + x.impuestos.Sum(t => t.porcentaje * x.total / 100)),
                    idCompra = null,
                    FechaCreacion = DateTime.Now,
                    UltimaModificacion = DateTime.Now,
                    UsuarioAutoriza = null,
                    FechaAutorizacion = null,
                    ProductosOrden = productsSupplier
                };
                ordersBySupplier.Add(currentOrder);
            }
            _orderPurchasePresenter.AddOrders(ordersBySupplier);
            productsToBuyList.Clear();
        }

        private void saveSupplier()
        {
            if (supplierToSave.idProveedor.Equals(0))
            {
                supplierToSave = _orderPurchasePresenter.SaveProveedor(supplierToSave).Result;
                if (supplierToSave != null && supplierToSave.estatus)
                    suppliersList.Add(supplierToSave);
            }
            else
            {
                if (!supplierToSave.estatus)
                {
                    suppliersList.Remove(supplierToSave);
                    _orderPurchasePresenter.SaveProveedor(supplierToSave);
                }
                else
                {
                    supplierToSave = _orderPurchasePresenter.SaveProveedor(supplierToSave).Result;
                    suppliersList.Clear();  
                    suppliersList.AddRange(_orderPurchasePresenter.GetSuppliersWithProducts());
                }
            }
        }
        #endregion

        #region Read data
        private void searchUserTxtBox_KeyUp(object sender, KeyEventArgs e)
        {
            string supplierTxt = (sender as TextBox).Text;
            if (e.Key == Key.Enter && supplierTxt != "")
            {
                suppliersList.Clear();
                suppliersList.AddRange(_orderPurchasePresenter.GetSuppliersByName(supplierTxt));
            }
        }

        private void searchUserTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string txtBoxText = (sender as TextBox).Text;
            if (txtBoxText == string.Empty)
            {
                suppliersList.Clear();
                suppliersList.AddRange(_orderPurchasePresenter.GetSuppliersWithProducts());
            }
        }
        #endregion

        #region Methods form
        private void ProductsToBuyList_ListChanged(object sender, ListChangedEventArgs e)
        {
            subtotal = productsToBuyList.Sum(x => x.total);
            impuestos = productsToBuyList.Sum(x => x.impuestos.Sum(t => t.porcentaje * x.total / 100));
            total = subtotal + impuestos;
            subtotalText.Text = subtotal.ToString("N2");
            impuestosText.Text = impuestos.ToString("N2");
            totalLabel.Text = total.ToString("N2");
        }
        private void textPago_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                float quantity = 1;
                if (textPago.Text.Contains('*'))
                {
                    string[] cantidad = textPago.Text.Split('*');
                    float.TryParse(cantidad[0], out quantity);
                    textPago.Text = cantidad[1];
                }
                ProductToBuy productResult = _orderPurchasePresenter.GetProductWithSuppliers(textPago.Text);
                if (productResult != null)
                {
                    addProductToBuy(productResult, quantity);
                }
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                updateProductToBuy(sender);
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            updateProductToBuy(sender);
        }

        private void textPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                updateProductPrice(sender);
            }
        }

        private void textPrice_LostFocus(object sender, RoutedEventArgs e)
        {
            updateProductPrice(sender);
        }

        private void ComboBox_TargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            ComboBox suppliers = (ComboBox)sender;
            ProductToBuy product = suppliers.BindingGroup.Items[0] as ProductToBuy;
            if (product.idProveedor == 0)
            {
                suppliers.SelectedIndex = 0;
            }
            else
            {
                suppliers.SelectedValue = product.idProveedor;
            }
        }

        private void finishBuying_Click(object sender, RoutedEventArgs e)
        {
            if (total > 0)
            {
                Compras compra = new Compras
                {
                    Subtotal = subtotal,
                    Impuestos = impuestos,
                    Total = total,
                    idUsuario = App._userApplication.idUsuario,
                    Estatus = 'A',
                    Fecha = DateTime.Now,
                    idAlmacen = (int)ComboWarehouse.SelectedValue,
                    ProductosCompra = (from producto in productsToBuyList select new ProductosCompra { idProducto = producto.idProducto, Cantidad = producto.quantity, Monto = producto.total }).ToList()
                };
                compra = _orderPurchasePresenter.AddCompra(idCurrentOrder, compra).Result;
                productsToBuyList.Clear();
                idCurrentOrder = 0;
                generateOrderButton.Content = "GENERAR \nORDEN(ES)";
                cancelOrder = false;
            }
        }

        private void buttonOrder_Click(object sender, RoutedEventArgs e) 
        {
            tabControlOrder.SelectedIndex = 0;
            checkTodas.IsChecked = true;
            ordersList.Clear();
            ordersList.AddRange(_orderPurchasePresenter.GetAllOrders());
        } 

        private void buttonBuy_Click(object sender, RoutedEventArgs e)
        {
            tabControlOrder.SelectedIndex = 1;
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox suppliers = (ComboBox)sender;
            ProductToBuy product = suppliers.BindingGroup.Items[0] as ProductToBuy;
            product.idProveedor = (suppliers.SelectedItem as ProveedorDto).idProveedor;
        }

        private void OrdersHome_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            Ordenes currentOrder = dataGrid.SelectedItem as Ordenes;
            if(currentOrder != null)
            {
                idCurrentOrder = currentOrder.idOrden;
                if (currentOrder.estatus == 'S')
                {
                    if (MessageBox.Show("La orden ya fue surtida, desea crear una a partir de ella?", "Alerta", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        loadInfoOrder(currentOrder);
                    }
                }
                else
                {
                    loadInfoOrder(currentOrder);
                }
            }
        }

        private void loadInfoOrder(Ordenes currentOrder)
        {
            IEnumerable<ProductToBuy> productsResult = _orderPurchasePresenter.GetProductsByList(currentOrder.ProductosOrden.ToList());
            productsResult = productsResult.Select(s => { ProductosCompra productoAux = (from producto in currentOrder.ProductosOrden where producto.idProducto.Equals(s.idProducto) select producto).SingleOrDefault(); s.idProveedor = currentOrder.idProveedor; s.quantity = productoAux.Cantidad; s.total = productoAux.Monto; return s; }).ToList();
            productsToBuyList.Clear();
            productsToBuyList.AddRange(productsResult);
            if (currentOrder != null)
            {
                tabControlOrder.SelectedIndex = 1;
                if (currentOrder.estatus != 'S')
                {
                    cancelOrder = true;
                    generateOrderButton.Content = "CANCELAR";
                }
                else
                {
                    cancelOrder = false;
                    generateOrderButton.Content = "GENERAR \nORDEN(ES)";
                }
            }
        }
                                                                                                                              
        private void suppliersGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            supplierToSave = dataGrid.SelectedItem as ProveedorEntity;
            txtNameSupplier.Text = supplierToSave.nombre;
            txtRepresentative.Text = supplierToSave.representante;
            txtPhones.Text = supplierToSave.telefonos;
            txtEmail.Text = supplierToSave.correos;
            txtWeb.Text = supplierToSave.paginaWeb;
            txtComments.Text = supplierToSave.comentarios;
            checkEstatus.IsChecked = supplierToSave.estatus;
        }

        private void cleanView()
        {
            txtNameSupplier.Text = string.Empty;
            txtRepresentative.Text = string.Empty;
            txtPhones.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtWeb.Text = string.Empty;
            txtComments.Text = string.Empty;
            checkEstatus.IsChecked = true;
            searchSupplierTextBox.Text = string.Empty;
            supplierToSave = new ProveedorEntity();
        }
         

        private void buttonSuppliers_Click(object sender, RoutedEventArgs e)
        {
            checkEstatus.IsChecked = true;
            tabControlOrder.SelectedIndex = 2;
        }
        private void saveSupplierButton_Click(object sender, RoutedEventArgs e)
        {
            supplierToSave.nombre = txtNameSupplier.Text;
            supplierToSave.representante = txtRepresentative.Text;
            supplierToSave.telefonos = txtPhones.Text;
            supplierToSave.correos = txtEmail.Text;
            supplierToSave.paginaWeb = txtWeb.Text;
            supplierToSave.comentarios = txtComments.Text;
            supplierToSave.estatus = (bool)checkEstatus.IsChecked;
            saveSupplier();
            cleanView();
        }

        private void activateSupplierBtn_Click(object sender, RoutedEventArgs e)
        {
            supplierToSave = suppliersGrid.SelectedItem as ProveedorEntity;
            if(supplierToSave != null)
            {
                supplierToSave.estatus = true;
                saveSupplier();
                cleanView();
            }
        }

        private void deactivateSupplierBtn_Click(object sender, RoutedEventArgs e)
        {
            supplierToSave = suppliersGrid.SelectedItem as ProveedorEntity;
            if (supplierToSave != null)
            {
                supplierToSave.estatus = false;
                saveSupplier();
                cleanView();
            }
        }

        private void comboSuppliersGral_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            productsToBuyList.Clear();
            if(comboSuppliersGral.SelectedIndex != -1)
                productsToBuyList.AddRange(_orderPurchasePresenter.GetProductsSuggestToBuy((int)comboSuppliersGral.SelectedValue));
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            cleanView();
        }

        private void addProduct_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNameSupplier.Text))
            {
                ProductDealer productDealer = new ProductDealer(_orderPurchasePresenter, txtNameSupplier.Text, supplierToSave);
                productDealer.ShowDialog();
                supplierToSave.productos = productDealer.ProductsSupplier;
                supplierToSave.productosWithDepartment = productDealer.ProductsDepartment;
            }
        }
        private void comboEstatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = (ComboBox)(sender);
            Ordenes currentOrder = combo.DataContext as Ordenes;
            char estatus = (char)combo.SelectedValue;
            combo.IsEnabled = estatus == 'S' || estatus == 'C' ? false : true;
            if (e.RemovedItems.Count > 0)
            {
                currentOrder.estatus = (char)combo.SelectedValue;
                _orderPurchasePresenter.UpdateOrder(new Ordenes
                {
                    idOrden = currentOrder.idOrden,
                    estatus = currentOrder.estatus,
                    FechaCreacion = currentOrder.FechaCreacion,
                    idCompra = currentOrder.idCompra,
                    idProveedor = currentOrder.idProveedor,
                    NumeroArticulos = currentOrder.NumeroArticulos,
                    Total = currentOrder.Total,
                    UltimaModificacion = DateTime.Now
                });
            }
        }

        private void initialDateSearch_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(initialDateSearch.SelectedDate > finalDateSearch.SelectedDate)
                finalDateSearch.SelectedDate = initialDateSearch.SelectedDate;

        }
        private void finalDateSearch_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (finalDateSearch.SelectedDate < initialDateSearch.SelectedDate)
                finalDateSearch.SelectedDate = initialDateSearch.SelectedDate;
        }

        private void Suggestions_Checked(object sender, RoutedEventArgs e)
        {
            comboSuppliersGral.IsEnabled = (bool)Suggestions.IsChecked;
            comboSuppliersGral.SelectedIndex = -1;
            productsToBuyList.Clear();
            if ((bool)Suggestions.IsChecked)
                productsToBuyList.AddRange(_orderPurchasePresenter.GetProductsSuggestToBuy(0));
        }

        private void checkCancelada_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)checkCancelada.IsChecked)
            {
                ordersList.AddRange(_orderPurchasePresenter.GetOrdersByEstatusWithSupplier('C'));
            }
            else
            {
                checkTodas.IsChecked = false;
                List<Ordenes> ordersFilter = ordersList.Where(x => !x.estatus.Equals('C')).ToList();
                ordersList.Clear();
                ordersList.AddRange(ordersFilter);
            }
        }

        private void checkEntregada_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)checkEntregada.IsChecked)
            {
                ordersList.AddRange(_orderPurchasePresenter.GetOrdersByEstatusWithSupplier('S'));
            }
            else
            {
                checkTodas.IsChecked = false;
                List<Ordenes> ordersFilter = ordersList.Where(x => !x.estatus.Equals('S')).ToList();
                ordersList.Clear();
                ordersList.AddRange(ordersFilter);
            }
        }

        private void checkElaborada_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)checkElaborada.IsChecked)
            {
                ordersList.AddRange(_orderPurchasePresenter.GetOrdersByEstatusWithSupplier('E'));
            }
            else
            {

                checkTodas.IsChecked = false;
                List<Ordenes> ordersFilter = ordersList.Where(x => !x.estatus.Equals('E')).ToList();
                ordersList.Clear();
                ordersList.AddRange(ordersFilter);
            }
        }

        private void checkTodas_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)checkTodas.IsChecked)
            {
                checkEntregada.IsChecked = checkElaborada.IsChecked = checkCancelada.IsChecked = true;
                ordersList.Clear();
                ordersList.AddRange(_orderPurchasePresenter.GetAllOrders());
            }
        }
        #endregion

        private void ComboWarehouse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
