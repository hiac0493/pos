using checkpoint.Helpers;
using checkpoint.Views.Catalogs.Unit.Models;
using checkpoint.Views.Catalogs.Unit.Presenters;
using checkpoint.Views.Catalogs.Unit.Services;
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

namespace checkpoint.Views.Catalogs.Unit.Views
{
    /// <summary>
    /// Interaction logic for Unit.xaml
    /// </summary>
    public partial class Unit : UserControl
    {
        #region Properties
        //**************************************************
        //*             PROPERTIES
        //**************************************************
        private UnitPresenter _unitPresenter;
        Unidades unidadesToSave = new Unidades();
        BindingList<Unidades> unitsList = new BindingList<Unidades>();
        #endregion
        #region Constructor

        //**************************************************
        //*             CONSTRUCTOR
        //**************************************************
        public Unit()
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
            _unitPresenter = new UnitPresenter(new UnitsServices());
            unitsGrid.ItemsSource = unitsList;
            unitsList.AddRange(_unitPresenter.GetAllUnits());
            cleanView();
        }

        #region Write data

        private void saveUnit()
        {
            if (unidadesToSave.idUnidad.Equals(0))
            {
                unidadesToSave = _unitPresenter.SaveUnit(unidadesToSave).Result;
                if (unidadesToSave != null && unidadesToSave.Activo)
                    unitsList.Add(unidadesToSave);
            }
            else
            {
                if (!unidadesToSave.Activo)
                {
                    _unitPresenter.SaveUnit(unidadesToSave);
                    unitsList.Remove(unidadesToSave);
                }
                else
                {
                    unidadesToSave = _unitPresenter.SaveUnit(unidadesToSave).Result;
                    searchUnitTextBox.Text = string.Empty;
                    unitsList.Clear();
                    unitsList.AddRange(_unitPresenter.GetAllUnits());
                }
            }
        }

        #endregion
        //**************************************************
        //*             READ DATA
        //**************************************************

        #region Read data
        private void searchUnit_KeyUp(object sender, KeyEventArgs e)
        {
            string txtBoxText = (sender as TextBox).Text;
            if (e.Key == Key.Enter && txtBoxText != "")
            {
                unitsList.Clear();
                unitsList.AddRange(_unitPresenter.GetUnitsByName(txtBoxText));
            }
        }


        private void searchUnit_TextChanged(object sender, TextChangedEventArgs e)
        {
            string txtBoxText = (sender as TextBox).Text;
            if (txtBoxText == string.Empty)
            {
                unitsList.Clear();
                unitsList.AddRange(_unitPresenter.GetAllUnits());
            }
        }
        #endregion

        #region Methods form

        private void unitsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            unidadesToSave = dataGrid.SelectedItem as Unidades;
            txtNameUnit.Text = unidadesToSave.Descripcion;
            checkStatusUnit.IsChecked = unidadesToSave.Activo;
        }

        private void saveUnit_Click(object sender, RoutedEventArgs e)
        {
            unidadesToSave.Descripcion = txtNameUnit.Text;
            unidadesToSave.Activo = (bool)checkStatusUnit.IsChecked;
            saveUnit();
            cleanView();
        }

        private void cleanView()
        {
            txtNameUnit.Text = string.Empty;
            checkStatusUnit.IsChecked = true;
            unidadesToSave = new Unidades();
        }

        private void deactivateUnit_Click(object sender, RoutedEventArgs e)
        {
            unidadesToSave = unitsGrid.SelectedItem as Unidades;
            if (unidadesToSave != null)
            {
                unidadesToSave.Activo = false;
                saveUnit();
                cleanView();
            }
        }

        private void activeUnit_Click(object sender, RoutedEventArgs e)
        {
            unidadesToSave = unitsGrid.SelectedItem as Unidades;
            if (unidadesToSave != null)
            {
                unidadesToSave.Activo = true;
                saveUnit();
                cleanView();
            }
        }

        private void cancelUnit_Click(object sender, RoutedEventArgs e)
        {

            cleanView();
        }
        #endregion

    }
}
