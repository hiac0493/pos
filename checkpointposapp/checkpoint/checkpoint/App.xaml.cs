using checkpoint.Login;
using checkpoint.Resources;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using HttpClientCommunicationClassLibrary;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.Configuration;

using System.Windows;
using System.Windows.Media.Imaging;

namespace checkpoint
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        internal static HttpClientTools HttpTools = new HttpClientTools(true);
        internal static Usuarios _userApplication = new Usuarios();
        public delegate void InUseDelegate(bool IsActive);
        public static event InUseDelegate InUseEvent;
        public static MainWindow mainWindow = new MainWindow();

        public static void OnUseChanged(bool result)
        {
            mainWindow.OnUseChanged(result);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            InUseEvent = new InUseDelegate(OnUseChanged);
        }

        public App()
        {
            InitializeComponent();
            HttpTools = new HttpClientTools(true)
            {
                WebApiUrl = ConfigurationManager.AppSettings["webapiurl"],
                UserName = "hiac0493",
                PassWord = "patito"
            };
        }

        public static bool Login(string user, string password)
        {
            PosLogin login = new PosLogin();
            AuthRequest authRequest = new AuthRequest();
            authRequest.User = user;
            authRequest.Pwd = password;
            return login.isLogin(authRequest);
        }


    }
}