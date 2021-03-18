using checkpoint.Helpers;
using checkpoint.Views.Inventory.Models;
using checkpoint.Views.Inventory.Presenters;
using checkpoint.Views.Inventory.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace checkpoint.Views.Inventory.Views
{
    /// <summary>
    /// Lógica de interacción para Inventory.xaml
    /// </summary>
    public partial class Inventory : UserControl
    {
        #region Properties
        private InventoryPresenter _inventoryPresenter;
        BindingList<ProductoAlmacenDto> productoAlmacenList = new BindingList<ProductoAlmacenDto>();
        #endregion        
        #region Constructor        
        public Inventory()
        {
            InitializeComponent();
            InitializeFormWithData();
        }
        #endregion        
        #region Fill data on start
        private void InitializeFormWithData()
        {
            _inventoryPresenter = new InventoryPresenter(new InventoryServices());
            IEnumerable<Almacenes> almacenes = _inventoryPresenter.GetAllAlmacenes();
            almacenComboBox.ItemsSource = almacenes.ToList();
            almacenComboBox.SelectedIndex = 0;
            productosAlmacenGrid.ItemsSource = productoAlmacenList;
        }
        #endregion

        #region Write data
        #endregion

        #region Read data
        #endregion

        #region Methods form
        private void almacenComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox almacenes = (ComboBox)sender;
            Almacenes almancen = almacenes.SelectedItem as Almacenes;
            productoAlmacenList.AddRange(_inventoryPresenter.GetProductosByAlmacen(almancen.idAlmacen));
        }
        #endregion
    }
}
