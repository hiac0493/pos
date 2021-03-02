using checkpoint.Helpers;
using checkpoint.Views.Catalogs.Turns.Models;
using checkpoint.Views.Catalogs.Turns.Presenters;
using checkpoint.Views.Catalogs.Turns.Services;
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

namespace checkpoint.Views.Catalogs.Turns.Views
{
    /// <summary>
    /// Interaction logic for Turn.xaml
    /// </summary>
    public partial class Turn : UserControl
    {
        #region Properties
        //**************************************************
        //*             PROPERTIES
        //**************************************************
        private TurnPresenter _turnPresenter;
        Turnos turnosToSave = new Turnos();
        BindingList<Turnos> turnsList = new BindingList<Turnos>();
        #endregion
        #region Constructor

        //**************************************************
        //*             CONSTRUCTOR
        //**************************************************

        public Turn()
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
            _turnPresenter = new TurnPresenter(new TurnsServices());
            turnGrid.ItemsSource = turnsList;
            turnsList.AddRange(_turnPresenter.GetAllTurns());
            cleanView();
        }

        //**************************************************
        //*             FILL DATA ON START
        //**************************************************

        #region Write data
        private void saveTurn()
        {
            if (turnosToSave.IdTurno.Equals(0))
            {
                turnosToSave = _turnPresenter.SaveTurn(turnosToSave).Result;
                if (turnosToSave != null && turnosToSave.Estatus)
                    turnsList.Add(turnosToSave);
            }
            else
            {
                if (!turnosToSave.Estatus)
                {
                    _turnPresenter.SaveTurn(turnosToSave);
                    turnsList.Remove(turnosToSave);
                }
                else
                {
                    turnosToSave = _turnPresenter.SaveTurn(turnosToSave).Result;
                    searchTurnTextBox.Text = string.Empty;
                    turnsList.Clear();
                    turnsList.AddRange(_turnPresenter.GetAllTurns());
                }
            }
        }
        #endregion

        //**************************************************
        //*             READ DATA
        //**************************************************

        #region Read data

        private void searchTurn_KeyUp(object sender, KeyEventArgs e)
        {
            string txtBoxText = (sender as TextBox).Text;
            if (e.Key == Key.Enter && txtBoxText != "")
            {
                turnsList.Clear();
                turnsList.AddRange(_turnPresenter.GetTurnsByName(txtBoxText));
            }
        }

        private void searchTurn_TextChanged(object sender, TextChangedEventArgs e)
        {
            string txtBoxText = (sender as TextBox).Text;
            if (txtBoxText == string.Empty)
            {
                turnsList.Clear();
                turnsList.AddRange(_turnPresenter.GetAllTurns());
            }
        }

        #endregion

        #region Methods form

        private void turnGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            turnosToSave = dataGrid.SelectedItem as Turnos;
            txtName.Text = turnosToSave.Nombre;
            checkStatus.IsChecked = turnosToSave.Estatus;
        }

        private void saveTurn_Click(object sender, RoutedEventArgs e)
        {
            turnosToSave.Nombre = txtName.Text;
            turnosToSave.Estatus = (bool)checkStatus.IsChecked;
            saveTurn();
            cleanView();
        }

        private void cleanView()
        {
            txtName.Text = string.Empty;
            checkStatus.IsChecked = true;
            turnosToSave = new Turnos();
        }

        private void deactivateTurn_Click(object sender, RoutedEventArgs e)
        {
            turnosToSave = turnGrid.SelectedItem as Turnos;
            if (turnosToSave != null)
            {
                turnosToSave.Estatus = false;
                saveTurn();
                cleanView();
            }
        }

        private void activeTurn_Click(object sender, RoutedEventArgs e)
        {
            turnosToSave = turnGrid.SelectedItem as Turnos;
            if (turnosToSave != null)
            {
                turnosToSave.Estatus = true;
                saveTurn();
                cleanView();
            }
        }

        private void cancelturn_Click(object sender, RoutedEventArgs e)
        {
            cleanView();
        }

        #endregion







    }
}
