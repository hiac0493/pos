using checkpoint.Helpers;
using checkpoint.Views.Catalogs.Screens.Models;
using checkpoint.Views.Catalogs.Screens.Presenters;
using checkpoint.Views.Catalogs.Screens.Services;
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

namespace checkpoint.Views.Catalogs.Screens.Views
{
    /// <summary>
    /// Interaction logic for Screen.xaml
    /// </summary>
    public partial class Screen : UserControl
    {

        #region Properties
        //**************************************************
        //*             PROPERTIES
        //**************************************************
        private ScreenPresenter _ScreenPresenter;
        Pantallas pantallasToSave = new Pantallas();
        BindingList<Pantallas> screenList = new BindingList<Pantallas>();
        #endregion
        #region Constructor

        //**************************************************
        //*             CONSTRUCTOR
        //**************************************************

        public Screen()
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
            _ScreenPresenter = new ScreenPresenter(new ScreenServices());
            txtNivel.PreviewTextInput += txtNivel.OnlyNumbersValidationTextBox;
            screenGrid.ItemsSource = screenList;
            screenList.AddRange(_ScreenPresenter.GetAllPantallas());
            cleanView();
        }

        #region Write data

        private void saveScreen()
        {
            if (pantallasToSave.idPantalla.Equals(0))
            {
                pantallasToSave = _ScreenPresenter.SaveScreen(pantallasToSave).Result;
                if (pantallasToSave != null && pantallasToSave.Activo)
                    screenList.Add(pantallasToSave);
            }
            else
            {
                if (!pantallasToSave.Activo)
                {
                    _ScreenPresenter.SaveScreen(pantallasToSave);
                    screenList.Remove(pantallasToSave);
                }
                else
                {
                    pantallasToSave =  _ScreenPresenter.SaveScreen(pantallasToSave).Result;
                    searchScreenTextBox.Text = string.Empty;
                    screenList.Clear();
                    screenList.AddRange(_ScreenPresenter.GetAllPantallas());
                }
            }
        }

        #endregion
        //**************************************************
        //*             READ DATA
        //**************************************************

        #region Read data
        private void searchScreen_KeyUp(object sender, KeyEventArgs e)
        {
            string valueTxt = (sender as TextBox).Text;
            if (e.Key == Key.Enter && valueTxt != "")
            {
                screenList.Clear();
                screenList.AddRange(_ScreenPresenter.GetScreenByName(valueTxt));
            }
        }


        private void searchScreen_TextChanged(object sender, TextChangedEventArgs e)
        {
            string txtBoxText = (sender as TextBox).Text;
            if (txtBoxText == string.Empty)
            {
                screenList.Clear();
                screenList.AddRange(_ScreenPresenter.GetAllPantallas());
            }
        }
        #endregion

        #region Methods form

        private void screenGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            pantallasToSave = dataGrid.SelectedItem as Pantallas;
            
            txtName.Text = pantallasToSave.NombrePantalla;
            txtPanel.Text = pantallasToSave.TextoPanel;
            txtUrl.Text = pantallasToSave.Url;
            txtIcon.Text = pantallasToSave.Icono;
            txtNivel.Text = pantallasToSave.Nivel.ToString();
            checkStatus.IsChecked = pantallasToSave.Activo;
            checkSubScreen.IsChecked = pantallasToSave.SubPantalla;
        }

        private void saveScreen_Click(object sender, RoutedEventArgs e)
        {
            pantallasToSave.NombrePantalla = txtName.Text;
            pantallasToSave.TextoPanel = txtPanel.Text;
            pantallasToSave.Url = txtUrl.Text;
            pantallasToSave.Icono = txtIcon.Text;
            pantallasToSave.Nivel = Int32.Parse(txtNivel.Text);
            pantallasToSave.Activo = (bool)checkStatus.IsChecked;
            pantallasToSave.SubPantalla = (bool)checkSubScreen.IsChecked;
            saveScreen();
            cleanView();
        }

        private void cleanView()
        {
            txtName.Text = string.Empty;
            txtPanel.Text = string.Empty;
            txtUrl.Text = string.Empty;
            txtIcon.Text = string.Empty;
            txtNivel.Text = string.Empty;
            checkStatus.IsChecked = true;
            checkSubScreen.IsChecked = false;
            pantallasToSave = new Pantallas();
        }

        private void deactivateScreen_Click(object sender, RoutedEventArgs e)
        {
            pantallasToSave = screenGrid.SelectedItem as Pantallas;
            if (pantallasToSave != null)
            {
                pantallasToSave.Activo = false;
                saveScreen();
                cleanView();
            }
        }

        private void activeScreen_Click(object sender, RoutedEventArgs e)
        {
            pantallasToSave = screenGrid.SelectedItem as Pantallas;
            if (pantallasToSave != null)
            {
                pantallasToSave.Activo = true;
                saveScreen();
                cleanView();
            }
        }

        private void cancelScreen_Click(object sender, RoutedEventArgs e)
        {
            cleanView();
        }
        #endregion

        private void brandGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
