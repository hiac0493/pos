using checkpoint.Helpers;
using checkpoint.Views.Catalogs.Unit.Presenters;
using checkpoint.Views.Catalogs.UnitSat.Models;
using checkpoint.Views.Catalogs.UnitSat.Presenters;
using checkpoint.Views.Catalogs.UnitSat.Services;
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

namespace checkpoint.Views.Catalogs.UnitSat.Views
{
    /// <summary>
    /// Interaction logic for UnitSat.xaml
    /// </summary>
    public partial class UnitSat : UserControl
    {
        #region Properties
        //**************************************************
        //*             PROPERTIES
        //**************************************************
        private UnitSatPresenter _UnitSatPresenter;
        unidadesSat unidadesToSave = new unidadesSat();
        BindingList<unidadesSat> unitsSatList = new BindingList<unidadesSat>();
        #endregion
        #region Constructor

        //**************************************************
        //*             CONSTRUCTOR
        //**************************************************
        public UnitSat()
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
            _UnitSatPresenter = new UnitSatPresenter(new UnitSatServices());
            txtClave.PreviewTextInput += txtClave.OnlyNumbersValidationTextBox;
            unitsSatGrid.ItemsSource = unitsSatList;
            unitsSatList.AddRange(_UnitSatPresenter.GetAllUnitSat());
            cleanView();
        }

        #region Write data

        private void saveUnitSat()
        {
            if (unidadesToSave.idUnidadSat.Equals(0))
            {
                unidadesToSave = _UnitSatPresenter.SaveUnitSat(unidadesToSave).Result;
                if (unidadesToSave != null && unidadesToSave.Activo)
                    unitsSatList.Add(unidadesToSave);
            }
            else
            {
                if (!unidadesToSave.Activo)
                {
                    _UnitSatPresenter.SaveUnitSat(unidadesToSave);
                    unitsSatList.Remove(unidadesToSave);
                }
                else
                {
                    unidadesToSave = _UnitSatPresenter.SaveUnitSat(unidadesToSave).Result;
                    searchUnitSatTextBox.Text = string.Empty;
                    unitsSatList.Clear();
                    unitsSatList.AddRange(_UnitSatPresenter.GetAllUnitSat());
                }
            }
        }

        #endregion
        //**************************************************
        //*             READ DATA
        //**************************************************

        #region Read data
        private void searchUnitSat_KeyUp(object sender, KeyEventArgs e)
        {
            string valueTxt = (sender as TextBox).Text;
            if (e.Key == Key.Enter && valueTxt != "")
            {
                unitsSatList.Clear();
                unitsSatList.AddRange(_UnitSatPresenter.GetUnitSatByName(valueTxt));
            }
        }


        private void searchUnitSat_TextChanged(object sender, TextChangedEventArgs e)
        {
            string txtBoxText = (sender as TextBox).Text;
            if (txtBoxText == string.Empty)
            {
                unitsSatList.Clear();
                unitsSatList.AddRange(_UnitSatPresenter.GetAllUnitSat());
            }
        }
        #endregion

        #region Methods form

        private void unitsSatGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            unidadesToSave = dataGrid.SelectedItem as unidadesSat;
            txtClave.Text = unidadesToSave.ClaveUnidad;
            txtName.Text = unidadesToSave.Descripcion;
            checkStatus.IsChecked = unidadesToSave.Activo;
            
        }

        private void saveUnitSat_Click(object sender, RoutedEventArgs e)
        {
            unidadesToSave.ClaveUnidad = txtClave.Text;
            unidadesToSave.Descripcion = txtName.Text;
            unidadesToSave.Activo = (bool)checkStatus.IsChecked;
            saveUnitSat();
            cleanView();
        }

        private void cleanView()
        {
            txtClave.Text = string.Empty;
            txtName.Text = string.Empty;
            checkStatus.IsChecked = true;
            unidadesToSave = new unidadesSat();
        }

        private void deactivateUnitSat_Click(object sender, RoutedEventArgs e)
        {
            unidadesToSave = unitsSatGrid.SelectedItem as unidadesSat;
            if (unidadesToSave != null)
            {
                unidadesToSave.Activo = false;
                saveUnitSat();
                cleanView();
            }
        }

        private void activeUnitSat_Click(object sender, RoutedEventArgs e)
        {
            unidadesToSave = unitsSatGrid.SelectedItem as unidadesSat;
            if (unidadesToSave != null)
            {
                unidadesToSave.Activo = true;
                saveUnitSat();
                cleanView();
            }
        }

        private void cancelUnitSat_Click(object sender, RoutedEventArgs e)
        {
            cleanView();
        }
        #endregion
    }
}

