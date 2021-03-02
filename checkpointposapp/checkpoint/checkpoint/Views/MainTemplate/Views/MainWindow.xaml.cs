using checkpoint.Resources.SystemModels;
using checkpoint.Views.MainTemplate.Models;
using checkpoint.Views.MainTemplate.Presenters;
using checkpoint.Views.MainTemplate.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Linq;

namespace checkpoint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TabItem _tabUserPage;
        private MainTemplatePresenter _mainTemplatePresenter;
        public delegate void InUseDelegate(bool IsActive);
        public static event InUseDelegate InUseEvent;
        //Config xml = new Config();

        public MainWindow()
        {
            InitializeComponent();
            InUseEvent = new InUseDelegate(OnUseChanged);
            _mainTemplatePresenter = new MainTemplatePresenter(new MainTemplateServices());
            CashConfig();
            InitializeApplicationScreens();
            txtDateNow.Text = DateTime.Now.ToLongDateString().ToUpper();
            txtCurrentUser.Text = $"{App._userApplication.Nombres} {App._userApplication.ApellidoPaterno} {App._userApplication.ApellidoMaterno}";
            App.mainWindow = this;
        }

        public void OnUseChanged(bool IsActive)
        {
            leftPanel.IsEnabled = IsActive;
        }

        private void InitializeApplicationScreens()
        {
            List<Pantallas> screensResult = _mainTemplatePresenter.GetAllPrincipalPantallas();
            if (screensResult != null)
            {
                foreach (Pantallas screen in screensResult)
                {
                    ListViewItem screenAdd = (ListViewItem)XamlReader.Load(XmlReader.Create(new StringReader($"<ListViewItem Height=\"50\" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"> <StackPanel Orientation = \"Horizontal\"> <fa5:SvgAwesome xmlns:fa5=\"http://schemas.fontawesome.com/icons/\" Icon = \"{screen.Icono}\" Width = \"20\" Margin = \"30 0 30 0\" Foreground = \"White\"/> <TextBlock Text = \"{screen.TextoPanel}\" VerticalAlignment = \"Center\" Foreground = \"White\"/> </StackPanel> </ListViewItem> ")));
                    screenAdd.PreviewMouseLeftButtonUp += ScreenAdd_PreviewMouseLeftButtonUp;
                    screenAdd.Tag = screen;
                    screenList.Items.Add(screenAdd);
                }
            }
        }

        private void ScreenAdd_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListViewItem itemClick = (ListViewItem)sender;
            ViewsTab.Items.Clear();
            Pantallas currentScreen = itemClick.Tag as Pantallas;
            var userControls = App.LoadComponent(new Uri(currentScreen.Url, UriKind.Relative));
            _tabUserPage = new TabItem { Content = userControls };
            _tabUserPage.Tag = this;
            ViewsTab.Items.Add(_tabUserPage); 
            ViewsTab.Items.Refresh();
        }

        private void SvgAwesome_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Desea salir de la aplicación?", "Alerta", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void CashConfig()
        {
            SystemConfig config = _mainTemplatePresenter.cashConfig();
        }


    }
}
