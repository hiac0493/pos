using checkpoint.Helpers;
using checkpoint.Users.Models;
using checkpoint.Users.Presenters;
using checkpoint.Users.Services;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace checkpoint.Views.Users.Views
{
    /// <summary>
    /// Interaction logic for addUser.xaml
    /// </summary>

    public partial class addUser : UserControl
    {
        #region Properties
        //**************************************************
        //*             PROPERTIES
        //**************************************************
        private UserPresenter _userPresenter;
        UsuariosRH usuarioUpdate;
        BindingList<UsuariosRH> usersList = new BindingList<UsuariosRH>();
        #endregion
        #region Constructor
        //**************************************************
        //*             CONSTRUCTOR
        //**************************************************
        public addUser()
        {
            InitializeComponent();
            InitializeFormWithData();
        }
        #endregion
        #region Fill data on start
        //**************************************************
        //*             FILL DATA ON START
        //**************************************************
        private void InitializeFormWithData()
        {
            _userPresenter = new UserPresenter(new UsersServices());
            newUserNameTxtBox.PreviewTextInput += newUserNameTxtBox.OnlyLettersValidationTextBox;
            searchUserTxtBox.PreviewTextInput += searchUserTxtBox.OnlyLettersValidationTextBox;
            newUserPaternalLastName.PreviewTextInput += newUserPaternalLastName.OnlyLettersValidationTextBox;
            newUserMaternalLastName.PreviewTextInput += newUserMaternalLastName.OnlyLettersValidationTextBox;
            newUserUserNickname.PreviewTextInput += newUserUserNickname.LettersAndNumbersValidationTextBox;
            activateUpdateUserChkBox.IsChecked = true;

            getAllUsers();
            usersGrid.ItemsSource = usersList;
        }
        #endregion
        #region Write data
        //**************************************************
        //*             WRITE DATA
        //**************************************************
        private void usersGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            usuarioUpdate = (sender as DataGrid).SelectedValue as UsuariosRH;
            if (usuarioUpdate != null)
            {
                newUserNameTxtBox.Text = usuarioUpdate.nombres;
                newUserPaternalLastName.Text = usuarioUpdate.apellidoPaterno;
                newUserMaternalLastName.Text = usuarioUpdate.apellidoMaterno;
                newUserUserNickname.IsReadOnly = true;
                newUserUserNickname.Focusable = false;
                newUserUserNickname.Text = usuarioUpdate.nombreUsuario;
                activateUpdateUserChkBox.IsChecked = usuarioUpdate.activo == true ? true : false;
                newUserPassword.Password = usuarioUpdate.contraseña;
            }
        }
        private void newUserBtn_Click(object sender, RoutedEventArgs e)
        {
            if (usuarioUpdate != null)
            {
                if (usuarioUpdate.idUsuario.Equals(0))
                {
                    createNewUser();
                    getAllUsers();
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(newUserNameTxtBox.Text)
                       && !string.IsNullOrWhiteSpace(newUserPaternalLastName.Text)
                       && !string.IsNullOrWhiteSpace(newUserMaternalLastName.Text)
                       && !string.IsNullOrWhiteSpace(newUserUserNickname.Text)
                       && !string.IsNullOrWhiteSpace(newUserPassword.Password))
                    {
                        usuarioUpdate.nombres = newUserNameTxtBox.Text;
                        usuarioUpdate.apellidoPaterno = newUserPaternalLastName.Text;
                        usuarioUpdate.apellidoMaterno = newUserMaternalLastName.Text;
                        usuarioUpdate.contraseña = newUserPassword.Password;
                        usuarioUpdate.activo = activateUpdateUserChkBox.IsChecked == true ? true : false;
                        _userPresenter.SaveUser(usuarioUpdate);
                        if (usuarioUpdate != null && usuarioUpdate.activo == false)
                        {
                            usersList.Remove(usuarioUpdate);
                        }
                        cleanViewCreate();
                        usersList.ResetBindings();
                    }
                }
            }
            else
            {
                createNewUser();
            }
        }

        private void createNewUser()
        {
            if (!string.IsNullOrWhiteSpace(newUserNameTxtBox.Text)
               && !string.IsNullOrWhiteSpace(newUserPaternalLastName.Text)
               && !string.IsNullOrWhiteSpace(newUserMaternalLastName.Text)
               && !string.IsNullOrWhiteSpace(newUserUserNickname.Text)
               && !string.IsNullOrWhiteSpace(newUserPassword.Password))
            {
                UsuariosRH usuario = new UsuariosRH
                {
                    nombreUsuario = newUserUserNickname.Text,
                    apellidoMaterno = newUserMaternalLastName.Text,
                    apellidoPaterno = newUserPaternalLastName.Text,
                    nombres = newUserNameTxtBox.Text,
                    contraseña = newUserPassword.Password,
                    activo = true
                };
                UsuariosRH addUser = _userPresenter.SaveUser(usuario).Result;
                cleanViewCreate();
                usersList.Add(usuario);
            }
            usersGrid.SelectedIndex = -1;
        }

        private void CancelBtnClick(object sender, RoutedEventArgs e)
        {
            cleanViewCreate();
        }
        private void DeleteUserBtnClick(object sender, RoutedEventArgs e)
        {
            usuarioUpdate = usersGrid.SelectedItem as UsuariosRH;
            if (usuarioUpdate != null)
            {
                usuarioUpdate.activo = false;
                _userPresenter.SaveUser(usuarioUpdate);
                newUserNameTxtBox.Text = string.Empty;
                newUserPaternalLastName.Text = string.Empty;
                newUserMaternalLastName.Text = string.Empty;
                newUserUserNickname.Text = string.Empty;
                newUserPassword.Password = string.Empty;
                activateUpdateUserChkBox.IsChecked = false;
                searchUserTxtBox.Text = string.Empty;
                usersList.Remove(usuarioUpdate);

                usersGrid.SelectedIndex = -1;
            }
        }
        private void activateUserBtn_Click(object sender, RoutedEventArgs e)
        {
            usuarioUpdate = usersGrid.SelectedItem as UsuariosRH;
            if (usuarioUpdate != null)
            {
                usuarioUpdate.activo = true;
                _userPresenter.SaveUser(usuarioUpdate);
                cleanViewCreate();
                getAllUsers();

                usersGrid.SelectedIndex = -1;
            }
        }
        #endregion
        #region Read data
        //**************************************************
        //*             READ DATA
        //**************************************************
        private void getAllUsers()
        {
            usersList.Clear();
            usersList = new BindingList<UsuariosRH>(_userPresenter.GetAllUsuarios());
            usersGrid.ItemsSource = usersList;
        }
        private void SearchUserHandler(object sender, KeyEventArgs e)
        {
            string userTxt = (sender as TextBox).Text;
            if (e.Key == Key.Enter && userTxt != "")
            {
                usersList = new BindingList<UsuariosRH>(_userPresenter.SearchUsersByName(userTxt));
                usersGrid.ItemsSource = usersList;
            }
        }
        private void searchUserTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string txtBoxText = (sender as TextBox).Text;
            if (txtBoxText == string.Empty)
                getAllUsers();
        }
        #endregion
        #region Methods form
        //**************************************************
        //*             METHODS FORM
        //**************************************************
        private void cleanViewCreate()
        {
            newUserNameTxtBox.Text = string.Empty;
            newUserNameTxtBox.Focus();
            newUserPaternalLastName.Text = string.Empty;
            newUserMaternalLastName.Text = string.Empty;
            newUserUserNickname.Text = string.Empty;
            newUserPassword.Password = string.Empty;
            activateUpdateUserChkBox.IsChecked = true;
            searchUserTxtBox.Text = string.Empty;
            newUserUserNickname.IsReadOnly = false;
            newUserUserNickname.Focusable = true;
            usuarioUpdate = new UsuariosRH();
        }
        #endregion
    }
}
