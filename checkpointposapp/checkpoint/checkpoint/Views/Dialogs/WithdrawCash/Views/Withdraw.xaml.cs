using checkpoint.Views.Dialogs.WithdrawCash.Presenter;
using checkpoint.Views.Dialogs.WithdrawCash.Services;
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

namespace checkpoint.Views.Dialogs.WithdrawCash.Views
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
        #endregion
        public Withdraw()
        {
            _withdrawPresenter = new WithdrawPresenter(new WithdrawServices());
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
            }
            else
            {
                MessageBox.Show("Contraseña incorrecta", "", MessageBoxButton.OK);
            }
            
        }
    }
}
