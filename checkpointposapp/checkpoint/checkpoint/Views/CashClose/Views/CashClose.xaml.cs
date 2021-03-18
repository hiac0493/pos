using checkpoint.Helpers;
using checkpoint.Views.CashClose.Models;
using checkpoint.Views.CashClose.Presenters;
using checkpoint.Views.CashClose.Services;
using CrearTicketVenta;
using ESC_POS_USB_NET.Printer;
using System.ComponentModel;
using System.Windows.Controls;

using System.Windows.Input;

namespace checkpoint.Views.CashClose.Views
{
    /// <summary>
    /// Interaction logic for CashClose.xaml
    /// </summary>

    public partial class CashClose : UserControl
    {
        #region Properties
        //**************************************************
        //*             PROPERTIES
        //**************************************************
        private CashClosePresenter _cashClosePresenter;
        double totalTaxe, ivaTasa, totalReturns, ivaReturns;
        Cortes cortesToSave = new Cortes();
        Cortes cortesAux = new Cortes();
        BindingList<CortePagos> cortePagoList = new BindingList<CortePagos>();
        BindingList<VentaImpuestos> impuestoList = new BindingList<VentaImpuestos>();
        BindingList<TasaImpuesto> tasaList = new BindingList<TasaImpuesto>();
        BindingList<TasaImpuesto> returnList = new BindingList<TasaImpuesto>();
        #endregion

        #region Constructor

        //**************************************************
        //*             CONSTRUCTOR
        //**************************************************
        public CashClose()
        {
            _cashClosePresenter = new CashClosePresenter(new CashCloseServices());
            InitializeComponent();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            endCashClose();

        }
        #endregion

        #region Write data

        private void saveCashClose(Cortes cortesToSave)
        {
            cortesAux = _cashClosePresenter.SaveCashClose(cortesToSave).Result;
        }

        #endregion

        #region Methods form
        private void endCashClose()
        {
            cortesToSave = _cashClosePresenter.GetCurrentCashClose(4);
            cortesToSave.FolioVentaFin = _cashClosePresenter.GetLastFolio();
            cortesToSave.TotalVenta = _cashClosePresenter.GetTotalSale(4, cortesToSave.FolioVentaInicio, (long)cortesToSave.FolioVentaFin);
            
            paymentsGrid.ItemsSource = cortePagoList;
            cortePagoList.AddRange(_cashClosePresenter.GetAllPagosCorte(cortesToSave.FolioVentaInicio, (long)cortesToSave.FolioVentaFin, 4));
            
            taxesGrid.ItemsSource = impuestoList;
            impuestoList.AddRange(_cashClosePresenter.GetAllTaxes(cortesToSave.FolioVentaInicio, (long)cortesToSave.FolioVentaFin, 4));
            
            totalSale.Text = cortesToSave.TotalVenta.ToString("C2");
            
            totalTaxe = _cashClosePresenter.GetTotalTaxes(cortesToSave.FolioVentaInicio, (long)cortesToSave.FolioVentaFin, 4);
            totalTaxes.Text =  totalTaxe.ToString("C2");
            ivaGrid.ItemsSource = tasaList;
            tasaList.AddRange(_cashClosePresenter.GetTotalWithTaxes(cortesToSave.FolioVentaInicio, (long)cortesToSave.FolioVentaFin, 4));
            returnsGrid.ItemsSource = returnList;
            totalTasa.Text =  cortesToSave.TotalVenta.ToString("C2");

            ivaTasa = _cashClosePresenter.CalcIvaTasa(4,cortesToSave.FolioVentaInicio, (long)cortesToSave.FolioVentaFin);
            totalTasaIva.Text =  ivaTasa.ToString("C2");
            returnList.AddRange(_cashClosePresenter.GetReturnsWithTaxes(cortesToSave.FolioVentaInicio, (long)cortesToSave.FolioVentaFin, 4));

            totalReturns = _cashClosePresenter.GetTotalReturns(cortesToSave.FolioVentaInicio, (long)cortesToSave.FolioVentaFin, 4);

            totalReturn.Text = totalReturns.ToString("C2");

            ivaReturns = _cashClosePresenter.CalcIvaReturn(4, cortesToSave.FolioVentaInicio, (long)cortesToSave.FolioVentaFin);
            totalReturnIva.Text = ivaReturns.ToString("C2");
            saveCashClose(cortesToSave);
        }
        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.F11)
            {
                PrintCashClose();
            }
        }

        private void PrintCashClose()
        {
            CreateTicket ticket = new CreateTicket();
            Printer printer = new Printer("EPSON TM-T20II Receipt5");
            printer.AlignCenter();
            printer.Append("CORTE Z");
            printer.Append("N° " + cortesAux.IdCorte);

            printer.AlignLeft();
            printer.Append("Fecha "+ cortesAux.FechaInicio.ToString());
            printer.Append(cortesAux.Caja.Nombre + " " + cortesAux.Turno.Nombre);
            printer.Separator();
            printer.BoldMode("Total de venta: " + cortesAux.TotalVenta.ToString("C2"));
            printer.Separator();
            printer.PrintDocument();
            printer = new Printer("EPSON TM-T20II Receipt5");
            double sumTipoPago = 0;
            foreach (CortePagos tipoPago in cortePagoList)
            {
                ticket.TextoExtremos(tipoPago.TipoPago, tipoPago.Total.ToString("C2"));
                sumTipoPago += tipoPago.Total;
            }
            ticket.TextoCentro("");
            ticket.AgregarTotalesCentrado("Suma: ", (float)sumTipoPago);
            ticket.ImprimirTicket("EPSON TM-T20II Receipt5");

            printer.Separator();
            printer.BoldMode("Total de impuestos: " + totalTaxe.ToString("C2"));
            printer.Separator();
            printer.PrintDocument();
            printer = new Printer("EPSON TM-T20II Receipt5");
            double sumTipoPagoTax = 0;
            foreach (VentaImpuestos impuesto in impuestoList)
            {
                ticket.TextoExtremos(impuesto.TipoImpuesto, impuesto.Total.ToString("C2"));
                sumTipoPagoTax += impuesto.Total;
            }
            ticket.TextoCentro("");
            ticket.AgregarTotalesCentrado("Suma: ", (float)sumTipoPagoTax);
            ticket.ImprimirTicket("EPSON TM-T20II Receipt5");

            printer.Separator();
            printer.BoldMode("Total de venta: " + cortesAux.TotalVenta.ToString("C2"));
            printer.Separator();
            printer.PrintDocument();
            printer = new Printer("EPSON TM-T20II Receipt5");
            double sumTasa = 0;
            foreach (TasaImpuesto tasaTax in tasaList)
            {
                ticket.TextoExtremos(tasaTax.Nombre, tasaTax.Total.ToString("C2"));
                sumTasa += tasaTax.Total;
            }
            ticket.TextoCentro("");
            ticket.AgregarTotalesCentrado("Suma: ", (float)sumTasa);
            ticket.ImprimirTicket("EPSON TM-T20II Receipt5");

            printer.Separator();
            printer.BoldMode("Total de devoluciones: " + totalReturns.ToString("C2"));
            printer.Separator();
            printer.PrintDocument();
            printer = new Printer("EPSON TM-T20II Receipt5");
            double sumDevoluciones = 0;
            foreach (TasaImpuesto returns in returnList)
            {
                ticket.TextoExtremos(returns.Nombre, returns.Total.ToString("C2"));
                sumDevoluciones += returns.Total;
            }
            ticket.TextoCentro("");
            ticket.AgregarTotalesCentrado("Suma: ", (float)sumDevoluciones);
            ticket.ImprimirTicket("EPSON TM-T20II Receipt5");

            printer.FullPaperCut();
            printer.PrintDocument();
            cleanView();
        }
        #endregion

        #region Methods form

        private void cleanView()
        {
            cortesToSave = new Cortes();
        }

        //TODO: crear el ticket a partir de los datos del corte de endCashClose()

        #endregion

    }
}
