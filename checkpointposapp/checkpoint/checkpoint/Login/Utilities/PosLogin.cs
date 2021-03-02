using checkpoint.Resources;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace checkpoint.Login
{
    public class PosLogin
    {
        public static string errorMessage;
        private Usuarios GetUserByUserName(AuthRequest authRequest)
        {
            Usuarios user = new Usuarios();
            string url = WebApiMethods.GetUsuarioByUserName + authRequest.User;

            HttpStatusCode responseUser = App.HttpTools.HttpGetUser<Usuarios>(url, ref user, authRequest.User, authRequest.Pwd);
            if(responseUser == HttpStatusCode.OK)
            {
                App.HttpTools.UserName = authRequest.User;
                App.HttpTools.PassWord = authRequest.Pwd;
                return user;
            }
            else
            {
                if (responseUser == HttpStatusCode.Unauthorized)
                {
                    errorMessage = "El usuario no fue encontrado en la base de datos, corrija su cuenta de correo por favor.";
                }
                else
                {
                    // Server is down, notify admin
                    errorMessage = "No se pudo validar sus credenciales, notificar al Administrador. El servidor no responde!";
                }
                return null;
            }
        }

        public bool isLogin(AuthRequest authRequest)
        {
            bool login = false;

            Usuarios user = GetUserByUserName(authRequest);

            // User is Ok
            if (user != null)
            {
                // Verify Pwd
                if (user.Contraseña == authRequest.Pwd)
                {
                    if (user.Activo)
                    {
                        // Set the current user information to use across the entire application
                        App._userApplication = user;
                        login = true;
                    }
                    else
                    {
                        errorMessage = $"!El Usuario {user.NombreUsuario} NO está activo en el Sistema; por favor verifique en Recursos Humanos!";
                        login = false;
                    }
                }
                else
                {
                    errorMessage = "El password es erroneo, corrija por favor.";
                }
            }
            return login;
        }
    }
}
