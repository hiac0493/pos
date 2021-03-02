using checkpoint.Helpers;
using checkpoint.Views.Catalogs.Taxes.Models;
using checkpoint.Views.Catalogs.Taxes.Presenters;
using checkpoint.Views.Catalogs.Taxes.Services;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace checkpoint.Views.Catalogs.Taxes.Views
{
    /// <summary>
    /// Interaction logic for addTax.xaml
    /// </summary>
    public partial class addTax : UserControl
    {
        #region Properties
        //**************************************************
        //*             PROPERTIES
        //**************************************************
        private TaxesPresenter _taxesPresenter;
        BindingList<Impuestos> taxesList = new BindingList<Impuestos>();
        Impuestos impuestoUpdate = new Impuestos();
        #endregion
        #region Constructor
        //**************************************************
        //*             CONSTRUCTOR
        //**************************************************
        public addTax()
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
            _taxesPresenter = new TaxesPresenter(new TaxesServices());
            searchTaxTextBox.PreviewTextInput += searchTaxTextBox.OnlyLettersValidationTextBox;
            taxNameTextBox.PreviewTextInput += taxNameTextBox.OnlyLettersValidationTextBox;
            percentageTaxTextBox.TextBoxPropertiesConfigurations(2, 1, 1, TextAlignment.Left);
            percentageTaxTextBox.PreviewTextInput += percentageTaxTextBox.OnlyNumbersValidationTextBox;
            taxGrid.ItemsSource = taxesList;
            activateTaxChkBox.IsChecked = true;
            getAllTaxes();
        }

        #endregion
        #region Write data
        //**************************************************
        //*             WRITE DATA
        //**************************************************
        private void saveTaxBtn_Click(object sender, RoutedEventArgs e)
        {
            if (impuestoUpdate.idImpuesto.Equals(0))
            {
                createNewTax();
                getAllTaxes();
            }
            else
            {
                updateTax();
            }
        }

        private void updateTax()
        {
            if (!string.IsNullOrWhiteSpace(taxNameTextBox.Text) &&
                !string.IsNullOrWhiteSpace(percentageTaxTextBox.Text))
            {
                impuestoUpdate.Descripcion = taxNameTextBox.Text;
                impuestoUpdate.Porcentaje = int.Parse(percentageTaxTextBox.Text);
                impuestoUpdate.Estatus = activateTaxChkBox.IsChecked == true ? true : false;
                _taxesPresenter.saveTaxes(impuestoUpdate);
                if(impuestoUpdate != null && !impuestoUpdate.Estatus)
                {
                    taxesList.Remove(impuestoUpdate);
                }
                ClearViews();
                taxesList.ResetBindings();
            }
        }

        private void createNewTax()
        {
            if(!string.IsNullOrWhiteSpace(taxNameTextBox.Text) &&
                !string.IsNullOrWhiteSpace(percentageTaxTextBox.Text))
            {
                Impuestos impuestos = new Impuestos
                {
                    Descripcion = taxNameTextBox.Text,
                    Porcentaje = int.Parse(percentageTaxTextBox.Text),
                    Estatus = true
                };
                Impuestos addImpuesto = _taxesPresenter.saveTaxes(impuestos).Result;
                ClearViews();
                taxesList.Add(impuestos);
            }
        }
        #endregion
        #region Read data
        //**************************************************
        //*             READ DATA
        //**************************************************
        private void getAllTaxes()
        {
            taxesList.Clear();
            taxesList.AddRange(_taxesPresenter.getAllTaxes());
            taxGrid.ItemsSource = taxesList;
        }
        private void searchTaxTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTxt = (sender as TextBox).Text;
            if(searchTxt == string.Empty)
            {
                getAllTaxes();
            }
        }

        private void searchTaxTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            string searchTxt = (sender as TextBox).Text;
            if (e.Key == Key.Enter && !string.IsNullOrWhiteSpace(searchTxt))
            {
                taxesList = new BindingList<Impuestos>(_taxesPresenter.getTaxesByName(searchTxt));
                taxGrid.ItemsSource = taxesList;
            }
        }
        #endregion
        #region Methods form
        //**************************************************
        //*             METHODS FORM
        //**************************************************
        private void activateTaxBtn_Click(object sender, RoutedEventArgs e)
        {
            impuestoUpdate = taxGrid.SelectedItem as Impuestos;
            if (impuestoUpdate != null)
            {
                impuestoUpdate.Estatus = true;
                _taxesPresenter.saveTaxes(impuestoUpdate);
                ClearViews();
                getAllTaxes();
            }
        }

        private void deactivateTaxBtn_Click(object sender, RoutedEventArgs e)
        {
            impuestoUpdate = taxGrid.SelectedItem as Impuestos;
            if (impuestoUpdate != null)
            {
                impuestoUpdate.Estatus = false;
                _taxesPresenter.saveTaxes(impuestoUpdate);
                taxesList.Remove(impuestoUpdate);
                taxesList.ResetBindings();
                ClearViews();
            }
        }
        private void taxGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            impuestoUpdate = (sender as DataGrid).SelectedValue as Impuestos;
            if (impuestoUpdate != null)
            {
                taxNameTextBox.Text = impuestoUpdate.Descripcion;
                percentageTaxTextBox.Text = impuestoUpdate.Porcentaje.ToString();
                activateTaxChkBox.IsChecked = impuestoUpdate.Estatus == true ? true : false;
            }
        }
        private void cancelTaxBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearViews();
            getAllTaxes();
        }
        private void ClearViews()
        {
            taxNameTextBox.Text = string.Empty;
            searchTaxTextBox.Text = string.Empty;
            percentageTaxTextBox.Text = string.Empty;
            impuestoUpdate = new Impuestos();
        }
        #endregion
    }
}
