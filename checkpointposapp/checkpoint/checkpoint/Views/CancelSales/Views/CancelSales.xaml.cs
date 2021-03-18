using checkpoint.CancelSales.Models;
using checkpoint.Views.CancelSales.Presenters;
using checkpoint.Views.CancelSales.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace checkpoint.Views.CancelSales.Views
{
    /// <summary>
    /// Lógica de interacción para CancelSales.xaml
    /// </summary>
    public partial class CancelSales : UserControl
    {
        private CancelSalePresenter _cancelSalesPresenter;
        public CancelSales()
        {
            InitializeComponent();
            _cancelSalesPresenter = new CancelSalePresenter(new CancelSaleServices());
        }

        private void FolioVentaText_KeyDown(object sender, KeyEventArgs e)
        {
            if(!String.IsNullOrEmpty(FolioVentaText.Text.Trim()) && e.Key == Key.Enter)
            {
                Ventas venta = _cancelSalesPresenter.GetVentaByFolioVenta(long.Parse(FolioVentaText.Text.Trim()));
                if(venta != null && venta.folioVenta != 0)
                {
                    if (venta.estatus == 'A')
                        ProductsSaleGrid.ItemsSource = venta.productos;
                    else
                        MessageBox.Show($"La venta {venta.folioVenta} se encuentra cancelada");
                }
            }
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            borderProductsGrid.Height = this.ActualHeight - 165;
            ProductsSaleGrid.Height = borderProductsGrid.Height - 140;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Ventas ventaCancelada = _cancelSalesPresenter.CancelaVenta(long.Parse(FolioVentaText.Text.Trim()));
            if (ventaCancelada != null)
            {
                MessageBox.Show($"La venta {FolioVentaText.Text.Trim()} fue cancelada con exito");
                FolioVentaText.Text = string.Empty;
                ProductsSaleGrid.ItemsSource = null;
                FolioVentaText.Focus();
            }
        }

        private void CancelProductSale_Loaded(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            AddProductSale producto = button.DataContext as AddProductSale;
            if(producto != null)
            {
                button.IsEnabled = producto.estatus;
            } 
        }

        private void CancelProductSale_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
