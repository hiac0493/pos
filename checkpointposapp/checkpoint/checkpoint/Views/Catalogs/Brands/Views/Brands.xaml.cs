using checkpoint.Helpers;
using checkpoint.Views.Catalogs.Brands.Models;
using checkpoint.Views.Catalogs.Brands.Presenters;
using checkpoint.Views.Catalogs.Brands.Services;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace checkpoint.Views.Catalogs.Brands.Views
{
    /// <summary>
    /// Interaction logic for Brands.xaml
    /// </summary>
    public partial class Brands : UserControl
    {
        #region Properties
        //**************************************************
        //*             PROPERTIES
        //**************************************************
        private BrandPresenter _brandPresenter;
        Marcas marcasToSave = new Marcas();
        BindingList<Marcas> brandsList = new BindingList<Marcas>();
        #endregion
        #region Constructor

        //**************************************************
        //*             CONSTRUCTOR
        //**************************************************
        public Brands()
        {
            InitializeComponent();
            InitializeFormWithData();
        }
        #endregion
        //**************************************************
        //*             FILL DATA ON START
        //**************************************************
        private void InitializeFormWithData()
        {
            _brandPresenter = new BrandPresenter(new BrandsServices());
            brandsGrid.ItemsSource = brandsList;
            brandsList.AddRange(_brandPresenter.GetAllMarcas());
            cleanView();
        }

        #region Write data

        private void saveBrand()
        {
            if (marcasToSave.idMarca.Equals(0))
            {
                marcasToSave = _brandPresenter.SaveBrand(marcasToSave).Result;
                if (marcasToSave != null && marcasToSave.Activo)
                    brandsList.Add(marcasToSave);
            }
            else
            {
                if (!marcasToSave.Activo)
                {
                    _brandPresenter.SaveBrand(marcasToSave);
                    brandsList.Remove(marcasToSave);
                }
                else
                {
                    marcasToSave = _brandPresenter.SaveBrand(marcasToSave).Result;
                    searchBrandTextBox.Text = string.Empty;
                    brandsList.Clear();
                    brandsList.AddRange(_brandPresenter.GetAllMarcas());
                }
            }
        }

        #endregion
        //**************************************************
        //*             READ DATA
        //**************************************************

        #region Read data
        private void searchBrand_KeyUp(object sender, KeyEventArgs e)
        {
            string supplierTxt = (sender as TextBox).Text;
            if (e.Key == Key.Enter && supplierTxt != "")
            {
                brandsList.Clear();
                brandsList.AddRange(_brandPresenter.GetBrandsByName(supplierTxt));
            }
        }

        
        private void searchBrand_TextChanged(object sender, TextChangedEventArgs e)
        {
            string txtBoxText = (sender as TextBox).Text;
            if (txtBoxText == string.Empty)
            {
                brandsList.Clear();
                brandsList.AddRange(_brandPresenter.GetAllMarcas());
            }
        }
        #endregion

        #region Methods form

        private void brandsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            marcasToSave = dataGrid.SelectedItem as Marcas;
            txtName.Text = marcasToSave.Descripcion;
            checkStatus.IsChecked = marcasToSave.Activo;
        }

        private void saveBrand_Click(object sender, RoutedEventArgs e)
        {
            marcasToSave.Descripcion = txtName.Text;
            marcasToSave.Activo = (bool)checkStatus.IsChecked;
            saveBrand();
            cleanView();
        }

        private void cleanView()
        {
            txtName.Text = string.Empty;
            checkStatus.IsChecked = true;
            marcasToSave = new Marcas();
        }

        private void deactivateBrand_Click(object sender, RoutedEventArgs e)
        {
            marcasToSave = brandsGrid.SelectedItem as Marcas;
            if (marcasToSave != null)
            {
                marcasToSave.Activo = false;
                saveBrand();
                cleanView();
            }
        }

        private void activeBrand_Click(object sender, RoutedEventArgs e)
        {
            marcasToSave = brandsGrid.SelectedItem as Marcas;
            if (marcasToSave != null)
            {
                marcasToSave.Activo = true;
                saveBrand();
                cleanView();
            }
        }

        private void cancelBrand_Click(object sender, RoutedEventArgs e)
        {

            cleanView();
        }
        #endregion
    }
}