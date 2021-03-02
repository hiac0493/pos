using checkpoint.Helpers;
using checkpoint.Views.CashClose.Models;
using checkpoint.Views.withdrawCash.Models;
using checkpoint.Views.withdrawCash.Presenters;
using checkpoint.Views.withdrawCash.Services;
using CrearTicketVenta;
using ESC_POS_USB_NET.Printer;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace checkpoint.Views.withdrawCash.Views
{
    /// <summary>
    /// Lógica de interacción para Withdraw.xaml
    /// </summary>
    public partial class Withdraw : UserControl
    {
        #region Properties
        //**************************************************
        //*             PROPERTIES
        //**************************************************
        private WithdrawPresenter _withdrawPresenter;
        Retiros retirosToSave = new Retiros();
        Cortes cortesToSave = new Cortes();
        BindingList<Retiros> retirosList = new BindingList<Retiros>();
        private bool flag = false;
        private long idcorte = 0;
        #endregion

        public Withdraw()
        {
            _withdrawPresenter = new WithdrawPresenter(new WithdrawServices());
            InitializeComponent();
            cortesToSave = _withdrawPresenter.GetCurrentCashClose(App._userApplication.idUsuario);
            idcorte = cortesToSave.IdCorte;
            getWithdraws();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        #region Write data

        private void saveRetiro()
        {
            if (retirosToSave.IdRetiro.Equals(0))
            {
                retirosToSave = _withdrawPresenter.SaveRetiros(retirosToSave).Result;
                if (retirosToSave != null && retirosToSave.Estatus)
                {
                    retirosList.Add(retirosToSave);
                    PrintMethod();
                }

            }
            else
            {
                _withdrawPresenter.SaveRetiros(retirosToSave);
                retirosList.Remove(retirosToSave);
            }
        }

        private void PrintMethod()
        {
            CreateTicket ticket = new CreateTicket();
            Printer printer = new Printer("EPSON TM-T20II Receipt5");
            printer.AlignCenter();
            //IMAGEN
            /*Bitmap image = new Bitmap(Bitmap.FromFile("Icon.bmp"));
            printer.Image(image);*/
            printer.Append("RETIRO # " + retirosToSave.IdRetiro);
            printer.AlignLeft();
            string cashierName = App._userApplication.Nombres + " " + App._userApplication.ApellidoPaterno + " " + App._userApplication.ApellidoMaterno;
            printer.Append("CAJERO: " + cashierName);
            printer.Append("FECHA: " + retirosToSave.Hora);
            printer.NewLine();
            printer.Separator();
            printer.NewLine();
            printer.PrintDocument();
            
            ticket.AgregarTotalesCentrado("RETIRO:", (float)retirosToSave.Cantidad);
            ticket.TextoIzquierda("");
            ticket.lineasGuion();
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("COMENTARIOS: " + retirosToSave.Comentarios);
            ticket.TextoIzquierda("");
            ticket.lineasGuion();
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoExtremos("X", "X                   ");
            ticket.TextoExtremos("___________________", "____________________");
            ticket.TextoExtremos(cashierName, cashierName);
            ticket.TextoExtremos("    Supervisor", "Cajero       ");
            ticket.CortaTicket();
            ticket.ImprimirTicket("EPSON TM-T20II Receipt5");
        }

        private void getWithdraws()
        {
            
            withdrawGrid.ItemsSource = retirosList;
            retirosList.AddRange(_withdrawPresenter.GetAllRetiros(idcorte));
        }
        private void getIncomes()
        {

            withdrawGrid.ItemsSource = retirosList;
            retirosList.AddRange(_withdrawPresenter.GetAllCashIncomes(idcorte));
        }

        #endregion

        #region Methods form
        private void saveWithdrawBtn_Click(object sender, RoutedEventArgs e)
        {   
            retirosToSave.IdCorte = idcorte;
            retirosToSave.Hora = DateTime.Now;
            retirosToSave.Comentarios = observationsTextBox.Text;
            retirosToSave.Cantidad = Convert.ToDouble(priceTextBox.Text);
            retirosToSave.Tipo = flag ? 'D' : 'A';
            retirosToSave.Estatus = true;
            saveRetiro();
            cleanView();
        }

        private void deleteWithdrawBtn_Click(object sender, RoutedEventArgs e)
        {
            retirosToSave = withdrawGrid.SelectedItem as Retiros;
            if (retirosToSave != null)
            {
                retirosToSave.Estatus = false;
                saveRetiro();
                cleanView();
            }
        }

        private void exitCashBtn_Click(object sender, RoutedEventArgs e)
        {
            flag = false;
            titleRetiro.Text = "RETIROS DE EFECTIVO";
            titleGrid.Text = "RETIROS DURANTE EL TURNO";
            retirosList = new BindingList<Retiros>();
            getWithdraws();
        }
        private void incomeCashBtn_Click(object sender, RoutedEventArgs e)
        {
            flag = true;
            titleRetiro.Text = "ENTRADAS DE EFECTIVO";
            titleGrid.Text = "ENTRADAS DURANTE EL TURNO";
            retirosList = new BindingList<Retiros>();
            getIncomes();
        }
        private void cancelWithdrawBtn_Click(object sender, RoutedEventArgs e)
        {
            cleanView();
        }
        


        private void cleanView()
        {
            cortesToSave = new Cortes();
            retirosToSave = new Retiros();
            priceTextBox.Text = "";
            observationsTextBox.Text = "";
        }

        #endregion

    }
}
