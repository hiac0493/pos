using checkpoint.Helpers;
using checkpoint.Sales.Models;
using checkpoint.Sales.Presenters;
using checkpoint.Sales.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace checkpoint.Sales.Views
{
    /// <summary>
    /// Interaction logic for EndSale.xaml
    /// </summary>
    public partial class EndSale : Window
    {
        private static Ventas _venta = new Ventas();
        private static BindingList<VentaPagos> pagos = new BindingList<VentaPagos>();
        
        string totalControl = "";
        string referencia = "";
        private float _cambio;
        private float _pagado;
        private static int paymentType = 1;
        private bool exitWindow = false;
        public Ventas ventaFin
        { 
            get { return _venta; }
        }
        public float cambio
        {
            get { return _cambio; }
        }
        public float pagado
        {
            get { return _pagado; }
        }
        BindingList<ProductsGridSales> _products = new BindingList<ProductsGridSales>();

        public EndSale(Ventas venta, BindingList<ProductsGridSales> products)
        {
            InitializeComponent();
            this.KeyDown += OnKeyDownHandler;
            pagos = new BindingList<VentaPagos>();
            pagos.ListChanged += Pagos_ListChanged;
            textPagoEfectivo.PreviewTextInput += textPagoEfectivo.NumberValidationTextBox;
            textPagoCredito.PreviewTextInput += textPagoCredito.NumberValidationTextBox;
            textPagoTarjetas.PreviewTextInput += textPagoTarjetas.NumberValidationTextBox;
            totalPagoVales.PreviewTextInput += totalPagoVales.NumberValidationTextBox;
            _products = products;
            _venta = venta;
            totalControl = $"{textPagoEfectivo.Name}";
            totalLabel.Content = _products.Sum(x => x.Price * x.Quantity).ToString("C2");
            cambioLabel.Content = ((_products.Sum(x => x.Price * x.Quantity)) - pagos.Sum(x => x.cantidad)).ToString("C2");
            tabPay.SelectedIndex = 0;
        }

        private void Pagos_ListChanged(object sender, ListChangedEventArgs e)
        {
            float total = (_products.Sum(x => x.Price * x.Quantity) - pagos.Sum(x => x.cantidad));
            totalLabel.Content = total < 0 ? "$0.00" : total.ToString("C2");
            float efectivo = pagos.Where(x => x.referencia.Equals(string.Empty)).Sum(x => x.cantidad);
            _cambio = pagos.Sum(x => x.cantidad) - (_products.Sum(x => x.Price * x.Quantity));
            _cambio = _cambio < 0 ? 0 : cambio;
            _pagado = pagos.Sum(x => x.cantidad);
            cambioLabel.Content = (_cambio).ToString("C2");
        }

        private void Got_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(((TextBox)sender));
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F2:
                    tabPay.SelectedIndex = 0;
                    paymentType = 2;
                    totalControl = $"{textPagoEfectivo.Name}";
                    referencia = string.Empty;
                    break;
                case Key.F3:
                    tabPay.SelectedIndex = 0;
                    paymentType = 1;
                    totalControl = $"{textPagoEfectivo.Name}";
                    referencia = string.Empty;
                    break;
                case Key.F4:
                    tabPay.SelectedIndex = 1;
                    paymentType = 4;
                    totalControl = $"{textPagoCredito.Name}";
                    referencia = $"cuenta pagado con credito";
                    break;
                case Key.F5:
                    tabPay.SelectedIndex = 2;
                    paymentType = 5;
                    totalControl = $"{textPagoTarjetas.Name}";
                    referencia = $"referencia generada apartir de pinpad";
                    break;
                case Key.F6:
                    tabPay.SelectedIndex = 3;
                    paymentType = 3;
                    totalControl = $"{totalPagoVales.Name}";
                    referencia = $"{referenciaTextVales.Name}";
                    break;
                case Key.F12:
                    addPaymentToPaymentsList();
                    break;
                case Key.Enter:
                    addPaymentToPaymentsList();
                    break;
                case Key.Escape:
                    DialogResult = false;
                    break;
                default:
                    break;
            }
        }

        private void addPaymentToPaymentsList()
        {
            TextBox textoPagoCliente = (TextBox)this.FindName(totalControl);
            TextBox textoReferencia = !string.IsNullOrEmpty(referencia) ? (TextBox)this.FindName(referencia) : null;
            if (!string.IsNullOrEmpty(textoPagoCliente.Text))
            {
                pagos.Add(new VentaPagos { idTipoPago = paymentType, cantidad = (float)Convert.ToDouble(textoPagoCliente.Text), referencia = textoReferencia != null ? textoReferencia.Text : referencia });
                textoPagoCliente.Text = string.Empty;
                if ((_products.Sum(x => x.Price * x.Quantity) - pagos.Sum(x => x.cantidad)) <= 0)
                {
                    tabPay.IsEnabled = false;
                    exitWindow = true;
                }
            }
            else if (exitWindow)
            {
                _venta.pagos = pagos;
                pagos = new BindingList<VentaPagos>();
                DialogResult = true;
            }
            textoPagoCliente.Focus();
        }

        private void efectivoButton_Click(object sender, RoutedEventArgs e)
        {
            tabPay.SelectedIndex = 0;
            paymentType = 1;
            totalControl = $"{textPagoEfectivo.Name}";
        }

        private void creditoButton_Click(object sender, RoutedEventArgs e)
        {
            tabPay.SelectedIndex = 1;
            paymentType = 4;
            totalControl = $"{textPagoCredito.Name}";
        }

        private void tarjetasButton_Click(object sender, RoutedEventArgs e)
        {
            tabPay.SelectedIndex = 2;
            paymentType = 5;
            totalControl = $"{textPagoTarjetas.Name}";
        }

        private void valesButton_Click(object sender, RoutedEventArgs e)
        {
            tabPay.SelectedIndex = 3;
            paymentType = 3;
            totalControl = $"{totalPagoVales.Name}";
        } 

        private void cobrarButton_Click(object sender, RoutedEventArgs e)
        {
            addPaymentToPaymentsList();
        }
    }
}
