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

namespace checkpoint.Views.Login
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            login(userNameText.Text, passlog.Password);
        }

        private void login(string userName, string password)
        {
            if (!String.IsNullOrEmpty(userName.Trim()))
            {
                if (App.Login(userName.Trim(), password))
                {
                    MainWindow window = new MainWindow();
                    window.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña invalidos");
                }
            }
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                login(userNameText.Text, passlog.Password);
            }
        }
    }
}
