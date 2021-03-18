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

namespace checkpoint.Views.Dialogs
{
    /// <summary>
    /// Lógica de interacción para Question.xaml
    /// </summary>
    public partial class Question : Window
    {
        public Question(string question,bool ok, bool cancel)
        {
            InitializeComponent();
            cuestionTxt.Text = question;
            if(ok != true)
            {
                successCta.Visibility = Visibility.Collapsed;
            }
            if (cancel != true)
            {
                cancelCta.Visibility = Visibility.Collapsed;
            }

        }
        private void authorize_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }



    }
}
