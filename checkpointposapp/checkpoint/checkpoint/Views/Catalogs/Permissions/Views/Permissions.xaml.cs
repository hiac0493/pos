using checkpoint.Helpers;
using checkpoint.Users.Models;
using checkpoint.Views.Catalogs.Permissions.Models;
using checkpoint.Views.Catalogs.Permissions.Presenters;
using checkpoint.Views.Catalogs.Permissions.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace checkpoint.Views.Catalogs.Permissions.View
{
    /// <summary>
    /// Lógica de interacción para Permissions.xaml
    /// </summary>
    public partial class Permissions : UserControl
    {
        private PermissionsPresenter _permissionsPresenter;
        Usuarios currentUser;
        List<Pantallas> screensList = new List<Pantallas>();
        BindingList<Usuarios> usersList = new BindingList<Usuarios>();
        public Permissions()
        {
            InitializeComponent(); 
            InitializeFormWithData();
        }

        private void InitializeFormWithData()
        {
            _permissionsPresenter = new PermissionsPresenter(new PermissionsServices());
            usersList.AddRange(_permissionsPresenter.GetAllUsuariosWithPermissions());
            UserGrid.ItemsSource = usersList;
            screensList.AddRange(_permissionsPresenter.GetAllPantallas());
            searchUserTxtBox.PreviewTextInput += searchUserTxtBox.OnlyLettersValidationTextBox;
        }

        private void CheckScreen_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;
            Pantallas pantalla = check.DataContext as Pantallas;
            if (pantalla != null)
            {
                //meter procedimiento
                PantallasUsuario usuarioPantalla = currentUser.PantallasUsuario.Where(x => x.idPantalla.Equals(pantalla.idPantalla)).FirstOrDefault();
                if (usuarioPantalla != null)
                    check.IsChecked = true;
                else
                    check.IsChecked = false;
            }
        }

        private void UserGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ScreensGrid.ItemsSource = null;
            DataGrid dataGrid = (DataGrid)sender;
            currentUser = usersList.Where(x => x.idUsuario.Equals((dataGrid.SelectedItem as Usuarios).idUsuario)).SingleOrDefault();
            if (currentUser != null)
            {
                ScreensGrid.ItemsSource = screensList;
            }
        }

        private void CheckScreen_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;
            Pantallas currentScreen = check.DataContext as Pantallas;
            if (currentUser != null)
            {
                if ((bool)check.IsChecked)
                {
                    long screenToAdd = currentUser.PantallasUsuario.Where(x => x.idPantalla.Equals(currentScreen.idPantalla)).Select(x => x.idPantallasUsuario).SingleOrDefault();
                    if (screenToAdd == 0)
                    {
                        PantallasUsuario pantalla = new PantallasUsuario
                        {
                            idPantalla = currentScreen.idPantalla,
                            idUsuario = currentUser.idUsuario,
                        };
                        currentUser.PantallasUsuario.Add(pantalla);
                    }
                }
                else
                {
                    long screenToRemove = currentUser.PantallasUsuario.Where(x => x.idPantalla.Equals(currentScreen.idPantalla)).Select(x=>x.idPantallasUsuario).SingleOrDefault();
                    if (screenToRemove != 0)
                    {
                        _permissionsPresenter.DeleteImpuestoProductoById(screenToRemove);
                    }
                    currentUser.PantallasUsuario = currentUser.PantallasUsuario.Where(x => !x.idPantalla.Equals(currentScreen.idPantalla)).ToList();
                }
            }
        }

        private void buttonOrder_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            currentUser = _permissionsPresenter.UpdateUserPermissionsScreens(new PermissionsUserDto
            {
                idUsuario = currentUser.idUsuario,
                pantallasUsuario = currentUser.PantallasUsuario
            });
            Usuarios userUpdate = usersList.Where(x => x.idUsuario.Equals(currentUser.idUsuario)).SingleOrDefault();
            userUpdate.PantallasUsuario = currentUser.PantallasUsuario;
            usersList.ResetBindings();
            ScreensGrid.ItemsSource = null;
        }
    }
}
