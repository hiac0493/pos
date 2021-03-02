using checkpoint.CheckPrices.Models;
using checkpoint.CheckPrices.Presenters;
using checkpoint.CheckPrices.Services;
using checkpoint.Sales.Models;
using checkpoint.Sales.Presenters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

namespace checkpoint.Views.Sales.Views
{
    /// <summary>
    /// Lógica de interacción para CheckPrices.xaml
    /// </summary>
    public partial class CheckPrices : Window
    {
        private CheckPresenter _checkPresenter;
        PLUProductCheck product;
        private string _ProductPLU;
        public string productoPLU
        {
            get { return _ProductPLU; }
        }
        public CheckPrices()
        {
            InitializeComponent();
            this.KeyDown += OnKeyDownHandler;
            _checkPresenter = new CheckPresenter(new ProductsCheckServices());
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    DialogResult = false;
                    break;
            }
        }
        private void OnItemChanged(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                product = ((sender as ListBox).SelectedItem as PLUProductCheck);
                nameDescription.Text = product.nombre;
                PLUDescription.Text = product.pluProducto;
                QuantityDescription.Text = product.existencia.ToString();
                PriceDescription.Text = product.precioVenta.ToString();
                skuCheckText.Text = "";
                skuCheckText.Focus();
                resultStack.ItemsSource = null;
                resultStack.Items.Clear();
                resultStack.Visibility = Visibility.Collapsed;
            }
        }
        private void skuCheckText_KeyDown(object sender, KeyEventArgs e)
        {
            string query = (sender as TextBox).Text;

            if ((e.Key == Key.Enter && query != "" && query.Length >= 3) || (query.Length >= 3 && query != ""))
            {
                List<PLUProductCheck> productCheckListFromDB = _checkPresenter.GetProductsByName(skuCheckText.Text);
                if (productCheckListFromDB != null && productCheckListFromDB.Count > 0)
                {
                    resultStack.ItemsSource = productCheckListFromDB;
                    resultStack.DisplayMemberPath = "nombre";
                    resultStack.Visibility = Visibility.Visible;
                }
                else
                {
                    resultStack.ItemsSource = null;
                    resultStack.Items.Clear();
                    resultStack.Visibility = Visibility.Collapsed;
                }
            }
            if (query.Length < 3)
            {
                resultStack.ItemsSource = null;
                resultStack.Items.Clear();
                resultStack.Visibility = Visibility.Collapsed;
            }

            if (e.Key == Key.Enter && resultStack.Items.Count > 0)
            {
                resultStack.Focus();
                resultStack.SelectedIndex = 0;
                var key = Key.Enter;                    // Key to send
                var target = Keyboard.FocusedElement;    // Target element
                var routedEvent = Keyboard.KeyDownEvent; // Event to send
                target.RaiseEvent(
                new KeyEventArgs(
                       Keyboard.PrimaryDevice,
                       PresentationSource.FromVisual((Visual)target),
                       0,
                       key)
                { RoutedEvent = routedEvent }
                );
            }

            if (resultStack.IsVisible)
            {
                if (e.Key == Key.Down)
                {
                    resultStack.Focus();
                    if (resultStack.SelectedIndex < (resultStack.Items.Count - 1))
                        resultStack.SelectedIndex++;
                    e.Handled = true;
                }
                else if (e.Key == Key.Up)
                {
                    resultStack.Focus();
                    if (resultStack.SelectedIndex > 0)
                        resultStack.SelectedIndex--;
                    e.Handled = true;
                }
            }
        }

        private void resultStack_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            product = ((sender as ListBox).SelectedItem as PLUProductCheck);
            nameDescription.Text = product.nombre;
            PLUDescription.Text = product.pluProducto;
            QuantityDescription.Text = product.existencia.ToString();
            PriceDescription.Text = product.precioVenta.ToString();
            skuCheckText.Text = "";
            skuCheckText.Focus();
            resultStack.ItemsSource = null;
            resultStack.Items.Clear();
            resultStack.Visibility = Visibility.Collapsed;
        }

        private void buttonOrder_Click(object sender, RoutedEventArgs e)
        {
            if(product != null)
            returnProductPLU();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F7)
            {
                if (product != null)
                    returnProductPLU();
            }
        }

        private void returnProductPLU()
        {
            _ProductPLU = product.pluProducto;
            DialogResult = true;
        }
    }
}
