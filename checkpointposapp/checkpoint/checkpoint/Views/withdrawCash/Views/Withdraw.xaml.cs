using checkpoint.Helpers;
using checkpoint.Views.CashClose.Models;
using checkpoint.Views.Dialogs;
using checkpoint.Views.withdrawCash.Models;
using checkpoint.Views.withdrawCash.Presenters;
using checkpoint.Views.withdrawCash.Services;
using CrearTicketVenta;
using ESC_POS_USB_NET.Printer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

namespace checkpoint.Views.WithdrawCash.Views
{
    /// <summary>
    /// Lógica de interacción para Withdraw.xaml
    /// </summary>
    public partial class Withdraw : Window
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
        private double _totalEfectivo;
        Question dialog;
        Question dialog2;
        Question dialog3;
        #endregion
        public Withdraw(double totalEfectivo, bool cancel)
        {
            _withdrawPresenter = new WithdrawPresenter(new WithdrawServices());
            InitializeComponent();
            _totalEfectivo = totalEfectivo;
            CashAvailable.Text = _totalEfectivo.ToString("C2");
            if(cancel == false)
            {
                cancelWithdraw.Visibility = Visibility.Collapsed;
            }
            cortesToSave = _withdrawPresenter.GetCurrentCashClose(App._userApplication.idUsuario);
            idcorte = cortesToSave.IdCorte;
            getWithdraws();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        private void retirosWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F2:

                    if (cashBox.Text != "")
                    {
                        dialog = new Question("¿Esta seguro de terminar el retiro?", true, true);
                        if (dialog.ShowDialog() == true)
                        {
                            dialog.Close();
                            getDataWithdraw();
                            saveRetiro();
                            this.Close();
                        }
                    }
                    else
                    {
                        dialog2 = new Question("¡Ingresa una cantidad de efectivo!", true, false);
                        if (dialog2.ShowDialog() == true)
                        {
                            dialog2.Close();
                        }
                    }

                    break;
            }
        }

        #region Write data

        private void saveRetiro()
        {
            if (retirosToSave.IdRetiro.Equals(0))
            {
                retirosToSave = _withdrawPresenter.SaveRetiros(retirosToSave).Result;
                if (retirosToSave != null && retirosToSave.Estatus)
                {
                    CashAvailable.Text= _totalEfectivo.ToString("C2");
                    retirosList.Add(retirosToSave);
                    this.Close();
                    //PrintMethod();
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
            if (cashBox.Text != "")
            {
                dialog = new Question("¿Esta seguro de terminar el retiro?", true, true);
                if (dialog.ShowDialog() == true)
                {
                    dialog.Close();
                    getDataWithdraw();
                    saveRetiro();
                    this.Close();
                }
            }
            else
            {
                dialog2 = new Question("¡Ingresa una cantidad de efectivo!", true, false);
                if (dialog2.ShowDialog() == true)
                {
                   // dialog2.Hide();
                }
            }

        }

        private void getDataWithdraw()
        {
            retirosToSave.IdCorte = idcorte;
            retirosToSave.Hora = DateTime.Now.ToString("HH:mm");
            retirosToSave.Comentarios = messageBox.Text;
            retirosToSave.Cantidad = Convert.ToDouble(cashBox.Text);
            retirosToSave.Tipo = flag ? 'D' : 'A';
            retirosToSave.Estatus = true;
        }

        private void deleteWithdrawBtn_Click(object sender, RoutedEventArgs e)
        {
            dialog3 = new Question("¿Esta seguro de eliminar el retiro?", true, true);
            if (dialog3.ShowDialog() == true)
            {
                retirosToSave = withdrawGrid.SelectedItem as Retiros;
                if (retirosToSave != null)
                {
                    _totalEfectivo = _totalEfectivo + retirosToSave.Cantidad;
                    CashAvailable.Text = _totalEfectivo.ToString("C2");
                    retirosToSave.Estatus = false;
                    saveRetiro();
                    cleanView();
                }
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
            this.Close();
        }


        



        private void cleanView()
        {
            cortesToSave = new Cortes();
            retirosToSave = new Retiros();
            cashBox.Text = "";
            messageBox.Text = "";
        }

        #endregion
    }
}
