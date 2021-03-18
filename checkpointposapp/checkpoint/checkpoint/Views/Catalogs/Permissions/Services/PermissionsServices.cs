using checkpoint.Resources;
using checkpoint.Views.Catalogs.Permissions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace checkpoint.Views.Catalogs.Permissions.Services
{
    public class PermissionsServices : IPermissionsServices
    {
        public IEnumerable<Usuarios> GetAllUsuariosWithPermissions()
        {
            string webApiUrl = WebApiMethods.GetAllUsuariosWithPermissions;
            IList<Usuarios> usersList = new List<Usuarios>();
            var response = App.HttpTools.HttpGetList<Usuarios>(webApiUrl, ref usersList, "Error en la lectura de los usuarios y sus permisos");
            if (response == HttpStatusCode.OK)
                return usersList.ToList();
            else
                return null;
        }

        public IEnumerable<Pantallas> GetAllPantallas()
        {
            string webApiUrl = WebApiMethods.GetAllPantallas;
            IList<Pantallas> pantallasList = new List<Pantallas>();
            var ResponseOK = App.HttpTools.HttpGetList<Pantallas>(webApiUrl, ref pantallasList, "No se encontró la pantalla");
            if (ResponseOK == HttpStatusCode.OK)
            {
                return pantallasList.ToList();
            }
            else
            {
                return null;
            }
        }

        public void DeletePantallaUsuario(long idPantallaUsuario)
        {
            string webApiUrl = WebApiMethods.DeletePantallaUsuario;
            var result = App.HttpTools.HttpDeleteAsync(webApiUrl, idPantallaUsuario, "Error al intentar eliminar la pantalla al usuario");
        }

        public Usuarios UpdateUserPermissionsScreens(PermissionsUserDto permissions)
        {
            string webApiUrl = WebApiMethods.UpdateUserPermissionsScreens;
            Usuarios userResult = App.HttpTools.HttpPostObjectWithResponseDataAsync<PermissionsUserDto, Usuarios>(webApiUrl, permissions, "Error en la actualización de los permisos del usuario").Result;
            return userResult;
        }

        public IEnumerable<PermisosPantalla> GetPantallasByUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
