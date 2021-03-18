using checkpoint.Helpers;
using checkpoint.Views.Catalogs.CatalogSat.Models;
using checkpoint.Views.Catalogs.CatalogSat.Presenters;
using checkpoint.Views.Catalogs.CatalogSat.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace checkpoint.Views.Catalogs.CatalogSat.Views
{
    /// <summary>
    /// Interaction logic for CatalogSat.xaml
    /// </summary>
    public partial class CatalogSat : UserControl
    {

        #region Properties
        //**************************************************
        //*             PROPERTIES
        //**************************************************
        private CatalogSatPresenter  _CatalogSatPresenter;
        catalogoSat catalogoToSave = new catalogoSat();
        BindingList<catalogoSat> catalogSatList = new BindingList<catalogoSat>();
        #endregion
        #region Constructor

        //**************************************************
        //*             CONSTRUCTOR
        //**************************************************
        public CatalogSat()
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
            _CatalogSatPresenter = new CatalogSatPresenter(new CatalogSatServices());
            txtClave.PreviewTextInput += txtClave.OnlyNumbersValidationTextBox;
            catalogSatGrid.ItemsSource = catalogSatList;
            catalogSatList.AddRange(_CatalogSatPresenter.GetAllCatalogSat());
            cleanView();
        }

        #region Write data

        private void saveCatalogSat()
        {
            if (catalogoToSave.idCatalogoSat.Equals(0))
            {
                catalogoToSave = _CatalogSatPresenter.SaveCatalogSat(catalogoToSave);
                if (catalogoToSave != null && catalogoToSave.Activo)
                    catalogSatList.Add(catalogoToSave);
            }
            else
            {
                if (!catalogoToSave.Activo)
                {
                    _CatalogSatPresenter.SaveCatalogSat(catalogoToSave);
                    catalogSatList.Remove(catalogoToSave);
                }
                else
                {
                    catalogoToSave = _CatalogSatPresenter.SaveCatalogSat(catalogoToSave);
                    searchCatalogSatTextBox.Text = string.Empty;
                    catalogSatList.Clear();
                    catalogSatList.AddRange(_CatalogSatPresenter.GetAllCatalogSat());
                }
            }
        }

        #endregion
        //**************************************************
        //*             READ DATA
        //**************************************************

        #region Read data
        private void searchCatalogSat_KeyUp(object sender, KeyEventArgs e)
        {
            string valueTxt = (sender as TextBox).Text;
            if (e.Key == Key.Enter && valueTxt != "")
            {
                catalogSatList.Clear();
                catalogSatList.AddRange(_CatalogSatPresenter.GetCatalogSatByName(valueTxt));
            }
        }


        private void searchCatalogSat_TextChanged(object sender, TextChangedEventArgs e)
        {
            string txtBoxText = (sender as TextBox).Text;
            if (txtBoxText == string.Empty)
            {
                catalogSatList.Clear();
                catalogSatList.AddRange(_CatalogSatPresenter.GetAllCatalogSat());
            }
        }
        #endregion

        #region Methods form

        private void catalogSatGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            catalogoToSave = dataGrid.SelectedItem as catalogoSat;
            txtClave.Text = catalogoToSave.Clave;
            txtName.Text = catalogoToSave.Descripcion;
            checkStatus.IsChecked = catalogoToSave.Activo;

        }

        private void saveCatalogSat_Click(object sender, RoutedEventArgs e)
        {
            catalogoToSave.Clave = txtClave.Text;
            catalogoToSave.Descripcion = txtName.Text;
            catalogoToSave.Activo = (bool)checkStatus.IsChecked;
            saveCatalogSat();
            cleanView();
        }

        private void cleanView()
        {
            txtClave.Text = string.Empty;
            txtName.Text = string.Empty;
            checkStatus.IsChecked = true;
            catalogoToSave = new catalogoSat();
        }

        private void deactivateCatalogSat_Click(object sender, RoutedEventArgs e)
        {
            catalogoToSave = catalogSatGrid.SelectedItem as catalogoSat;
            if (catalogoToSave != null)
            {
                catalogoToSave.Activo = false;
                saveCatalogSat();
                cleanView();
            }
        }

        private void activeCatalogSat_Click(object sender, RoutedEventArgs e)
        {
            catalogoToSave = catalogSatGrid.SelectedItem as catalogoSat;
            if (catalogoToSave != null)
            {
                catalogoToSave.Activo = true;
                saveCatalogSat();
                cleanView();
            }
        }

        private void cancelCatalogSat_Click(object sender, RoutedEventArgs e)
        {
            cleanView();
        }
        #endregion
    }
}
