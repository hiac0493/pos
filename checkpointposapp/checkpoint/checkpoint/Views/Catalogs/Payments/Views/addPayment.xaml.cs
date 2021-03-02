using checkpoint.Helpers;
using checkpoint.Views.Catalogs.Payments.Models;
using checkpoint.Views.Catalogs.Payments.Presenters;
using checkpoint.Views.Catalogs.Payments.Services;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace checkpoint.Views.Catalogs.Payments.Views
{
    /// <summary>
    /// Interaction logic for addPayment.xaml
    /// </summary>
    public partial class addPayment : UserControl
    {
        #region Properties
        //**************************************************
        //*             PROPERTIES
        //**************************************************
        private PaymentsPresenter _paymentsPresenter;
        BindingList<TipoPago> tipoPagoList = new BindingList<TipoPago>();
        TipoPago tipoPagoUpdate = new TipoPago();
        #endregion
        #region Constructor
        //**************************************************
        //*             CONSTRUCTOR
        //**************************************************
        public addPayment()
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
            _paymentsPresenter = new PaymentsPresenter(new PaymentsServices());
            searchPaymentTextBox.PreviewTextInput += searchPaymentTextBox.OnlyLettersValidationTextBox;
            paymentNameTextBox.PreviewTextInput += paymentNameTextBox.OnlyLettersValidationTextBox;
            activatePaymentChkBox.IsChecked = true;
            paymentGrid.ItemsSource = tipoPagoList;
            getAllPayments();
        }
        #endregion
        #region Write data
        //**************************************************
        //*             WRITE DATA
        //**************************************************
        private void savePaymentBtn_Click(object sender, RoutedEventArgs e)
        {
            if (tipoPagoUpdate.idTipoPago.Equals(0))
            {
                createNewPaymentMethods();
                getAllPayments();
            }
            else
            {
                updatePaymentMethod();
            }
        }
        #endregion
        #region Read data
        //**************************************************
        //*             READ DATA
        //**************************************************
        private void searchPaymentTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTxtBox = (sender as TextBox).Text;
            if (searchTxtBox == string.Empty)
                getAllPayments();
        }
        private void searchPaymentTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            string searchTxt = (sender as TextBox).Text;
            if (e.Key == Key.Enter && !string.IsNullOrWhiteSpace(searchTxt))
            {
                tipoPagoList = new BindingList<TipoPago>(_paymentsPresenter.getPaymentByName(searchTxt));
                paymentGrid.ItemsSource = tipoPagoList;
            }
        }
        private void getAllPayments()
        {
            tipoPagoList.Clear();
            tipoPagoList.AddRange(_paymentsPresenter.getAllPayments());
            paymentGrid.ItemsSource = tipoPagoList;
        }
        #endregion
        #region Methods form
        //**************************************************
        //*             METHODS FORM
        //**************************************************
        private void updatePaymentMethod()
        {
            if (!string.IsNullOrWhiteSpace(paymentNameTextBox.Text))
            {
                tipoPagoUpdate.Descripcion = paymentNameTextBox.Text;
                tipoPagoUpdate.Estatus = activatePaymentChkBox.IsChecked == true ? true : false;
                _paymentsPresenter.savePayment(tipoPagoUpdate);
                if (tipoPagoUpdate != null && tipoPagoUpdate.Estatus == false)
                {
                    tipoPagoList.Remove(tipoPagoUpdate);
                }
                clearViews();
                tipoPagoList.ResetBindings();
            }
        }

        private void createNewPaymentMethods()
        {
            if (!string.IsNullOrWhiteSpace(paymentNameTextBox.Text))
            {
                TipoPago tipopago = new TipoPago
                {
                    Descripcion = paymentNameTextBox.Text,
                    Estatus = true
                };
                TipoPago addDepartamento = _paymentsPresenter.savePayment(tipopago).Result;
                clearViews();
                tipoPagoList.Add(tipopago);
            }
        }
        private void clearViews()
        {
            searchPaymentTextBox.Text = string.Empty;
            paymentNameTextBox.Text = string.Empty;
            tipoPagoUpdate = new TipoPago();
        }
        private void cancelPaymentBtn_Click(object sender, RoutedEventArgs e)
        {
            clearViews();
            getAllPayments();
        }
        private void activatePaymentBtn_Click(object sender, RoutedEventArgs e)
        {
            tipoPagoUpdate = paymentGrid.SelectedItem as TipoPago;
            if (tipoPagoUpdate != null)
            {
                tipoPagoUpdate.Estatus = true;
                _paymentsPresenter.savePayment(tipoPagoUpdate);
                clearViews();
                getAllPayments();
            }
        }
        private void deactivatePaymentBtn_Click(object sender, RoutedEventArgs e)
        {
            tipoPagoUpdate = paymentGrid.SelectedItem as TipoPago;
            if (tipoPagoUpdate != null)
            {
                tipoPagoUpdate.Estatus = false;
                _paymentsPresenter.savePayment(tipoPagoUpdate);
                tipoPagoList.ResetBindings();
                if (!tipoPagoUpdate.Estatus)
                {
                    tipoPagoList.Remove(tipoPagoUpdate);
                    clearViews();
                }
            }
        }
        private void paymentGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tipoPagoUpdate = (sender as DataGrid).SelectedValue as TipoPago;
            if (tipoPagoUpdate != null)
            {
                paymentNameTextBox.Text = tipoPagoUpdate.Descripcion;
                activatePaymentChkBox.IsChecked = tipoPagoUpdate.Estatus == true ? true : false;
            }
        }
        #endregion
    }
}
