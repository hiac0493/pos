using checkpoint.Views.MenuCruds.Models;
using checkpoint.Views.MenuCruds.Presenters;
using checkpoint.Views.MenuCruds.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;

namespace checkpoint.Views.MenuCruds
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
        TabItem _catalogTabItem;
        private MenuCrudPresenter _menuCrudPresenter;
        public Menu()
        {
            InitializeComponent();
            _menuCrudPresenter = new MenuCrudPresenter(new MenuCrudsServices());
            InitializeApplicationScreens();
        }

        private void InitializeApplicationScreens()
        {
            List<Pantallas> screensResult = _menuCrudPresenter.GetAllSubPantallas();
            foreach (Pantallas screen in screensResult)
            {

                Button buttonSubScreenAdd = (Button)XamlReader.Load(XmlReader.Create(new StringReader($"<Button Padding=\"5\" Margin = \"30 60 30 0\" Background = \"#0069c0\" BorderThickness = \"0\" Width = \"200\" Height = \"100\" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"> <StackPanel> <fa5:SvgAwesome xmlns:fa5=\"http://schemas.fontawesome.com/icons/\" Icon = \"{screen.Icono}\" Width = \"30\" Foreground = \"White\" Margin = \"0 0 0 10 \" /> <TextBlock Text = \"{screen.TextoPanel}\" Foreground = \"white\" FontSize = \"20\" /> </StackPanel></Button>")));
                buttonSubScreenAdd.Click += ButtonSubScreenAdd_Click;
                buttonSubScreenAdd.Tag = screen;
                buttonContainer.Children.Add(buttonSubScreenAdd);
            }
        }

        private void ButtonSubScreenAdd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button currentButton = (Button)(sender);
            Pantallas currentScreen = currentButton.Tag as Pantallas;
            var userControls = App.LoadComponent(new Uri(currentScreen.Url, UriKind.Relative));
            _catalogTabItem = new TabItem { Content = userControls };
            catalogsTab.Items.Add(_catalogTabItem);
            catalogsTab.Items.Refresh();
            catalogsTab.SelectedIndex = 1;
        }
    }
}
 