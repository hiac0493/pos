using checkpoint.CheckPrices.Models;
using checkpoint.CheckPrices.Presenters;
using checkpoint.CheckPrices.Services;
using checkpoint.Helpers;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace checkpoint.CheckPrices.Views
{
    /// <summary>
    /// Interaction logic for checkPrices.xaml
    /// </summary>
    public partial class checkPrices : Window
    {
        #region Properties
        //**************************************************
        //*             PROPERTIES
        //**************************************************
        private CheckPresenter _productCheckPresenter;
        #endregion
        #region Constructor
        //**************************************************
        //*             CONSTRUCTOR
        //**************************************************
        public checkPrices()
        {
            InitializeComponent();
            InitializeFormWithData();
        }
        #endregion
        #region Fill data on start
        //**************************************************
        //*             FILL DATA ON START
        //**************************************************
        private void InitializeFormWithData()
        {
            _productCheckPresenter = new CheckPresenter(new ProductsCheckServices());
            skuCheckText.PreviewTextInput += skuCheckText.OnlyNumbersValidationTextBox;
            skuCheckText.TextBoxPropertiesConfigurations(25, 1, 1, TextAlignment.Left);
        }
        #endregion
        #region Write data
        //**************************************************
        //*             WRITE DATA
        //**************************************************
        #endregion
        #region Read data
        //**************************************************
        //*             READ DATA
        //**************************************************
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            string query = (sender as TextBox).Text;


            if (e.Key == Key.Enter && query != "")
            {
                PLUProductCheck productExtractFromDB = _productCheckPresenter.GetProductByPLU(skuCheckText.Text);
                if (productExtractFromDB != null)
                {
                    nameDescription.Text = productExtractFromDB.nombre;
                    PLUDescription.Text = productExtractFromDB.pluProducto;
                    QuantityDescription.Text = productExtractFromDB.existencia.ToString();
                    PriceDescription.Text = productExtractFromDB.precioVenta.ToString();
                    skuCheckText.Text = string.Empty;
                }
            }
        }

        private void OnItemChanged(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PLUProductCheck product = ((sender as ListBox).SelectedItem as PLUProductCheck);

                nameDescription.Text = product.nombre;
                PLUDescription.Text = product.pluProducto;
                QuantityDescription.Text = product.existencia.ToString();
                PriceDescription.Text = product.precioVenta.ToString();

                nameCheckText.Text = "";
                nameCheckText.Focus();
                resultStack.ItemsSource = null;
                resultStack.Items.Clear();
                resultStack.Visibility = Visibility.Collapsed;
            }
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            string query = (sender as TextBox).Text;

            if ((e.Key == Key.Enter && query != "" && query.Length >= 3) || (query.Length >= 3 && query != ""))
            {
                List<PLUProductCheck> productCheckListFromDB = _productCheckPresenter.GetProductsByName(nameCheckText.Text);
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

            if (query.Length < 3)
            {
                resultStack.ItemsSource = null;
                resultStack.Items.Clear();
                resultStack.Visibility = Visibility.Collapsed;
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
        #endregion
        #region Methods form
        //**************************************************
        //*             METHODS
        //**************************************************
        #endregion
    }
}
