using checkpoint.Views.CashClose.Models;
using checkpoint.Views.CashClose.Presenters;
using checkpoint.Views.CashClose.Services;
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
using System.Windows.Shapes;

namespace checkpoint.Views.CashClose
{
    /// <summary>
    /// Interaction logic for OpenCash.xaml
    /// </summary>
    public partial class OpenCash : Window
    {
        #region Properties
        //**************************************************
        //*             PROPERTIES
        //**************************************************
        private CashClosePresenter _cashClosePresenter;
        Cortes cortesToSave = new Cortes();
        #endregion

        #region Constructor

        //**************************************************
        //*             CONSTRUCTOR
        //**************************************************
        public OpenCash()
        {
            _cashClosePresenter = new CashClosePresenter(new CashCloseServices());
            InitializeComponent();
        }

        #endregion


        #region Write data

        private void saveCashClose()
        {
            cortesToSave = _cashClosePresenter.SaveCashClose(cortesToSave).Result;
        }

        #endregion

        #region Methods form
        private void openCash_Click(object sender, RoutedEventArgs e)
        {
            int folio = _cashClosePresenter.GetLastFolio();
            cortesToSave.IdTurno = 1;
            cortesToSave.IdCaja = 1;
            cortesToSave.IdUsuario = 4;
            cortesToSave.FolioVentaInicio = folio;
            cortesToSave.FolioVentaFin = folio;
            cortesToSave.FondoCaja = Convert.ToDouble(cashMoney.Text);
            cortesToSave.FechaInicio = new DateTime(2020,12,03);
            cortesToSave.FechaFinal = new DateTime(2020, 12, 03);
            cortesToSave.TotalVenta = 1;
            cortesToSave.TotalUtilidad = 1;
            saveCashClose();
            cleanView();
        }

        #endregion

        #region Methods form

        private void cleanView()
        {
            cortesToSave = new Cortes();
        }

        #endregion
    }
}
