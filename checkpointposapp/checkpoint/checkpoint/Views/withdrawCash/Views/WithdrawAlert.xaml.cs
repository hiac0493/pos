using checkpoint.Views.withdrawCash.Presenters;
using checkpoint.Views.withdrawCash.Services;
using checkpoint.Views.WithdrawCash.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace checkpoint.Views.WithdrawCash
{
    /// <summary>
    /// Lógica de interacción para WithdrawAlert.xaml
    /// </summary>
    public partial class WithdrawAlert : Window
    {
        #region Properties
        //**************************************************
        //*             PROPERTIES
        //**************************************************
        private WithdrawPresenter _withdrawPresenter;
        private double _totalEfectivo;
        #endregion

        public WithdrawAlert(double totalEfectivo,bool cancel)
        {
            _withdrawPresenter = new WithdrawPresenter(new WithdrawServices());
            _totalEfectivo = totalEfectivo;
            InitializeComponent();
        }

        private void authorize_Click(object sender, RoutedEventArgs e)
        {
            string user = userNameText.Text;
            string pass = passlog.Password.ToString();
            bool flag = _withdrawPresenter.GetUserAdmin(user, pass);
            if (flag)
            {
                this.Close();
                Withdraw retiro = new Withdraw(_totalEfectivo, false);
                retiro.ShowDialog();
            }
            else
            {
                MessageBox.Show("Contraseña incorrecta", "", MessageBoxButton.OK);
            }

        }
    }
}
