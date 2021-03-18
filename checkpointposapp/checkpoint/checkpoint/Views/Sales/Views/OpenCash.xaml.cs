using checkpoint.Views.CashClose.Models;
using checkpoint.Views.CashClose.Presenters;
using checkpoint.Views.CashClose.Services;
using System;
using System.Windows;

namespace checkpoint.Sales.Views
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
            if (cashMoney.Text != null)
            {
                int folio = _cashClosePresenter.GetLastFolio();
                cortesToSave.IdTurno = 1;
                cortesToSave.IdCaja = 1;
                cortesToSave.IdUsuario = App._userApplication.idUsuario;
                cortesToSave.FolioVentaInicio = folio;
                cortesToSave.FolioVentaFin = null;
                cortesToSave.FondoCaja = Convert.ToDouble(cashMoney.Text);
                cortesToSave.FechaInicio = new DateTime(2020,12,03);
                cortesToSave.FechaFinal = new DateTime(2020, 12, 03);
                cortesToSave.TotalVenta = 1;
                cortesToSave.TotalUtilidad = 1;
                saveCashClose();
                cleanView();
            }
        }

        #endregion

        #region Methods form

        private void cleanView()
        {
            cortesToSave = new Cortes(); 
            DialogResult = true;
        }

        #endregion
    }
}
