using checkpoint.Resources;
using checkpoint.Users.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace checkpoint.Users.Services
{
    public class UsersServices : IUsersServices
    {
        public List<UsuariosRH> SearchUsersByName(string name)
        {
            string webApiUrl = WebApiMethods.GetUsuarioByName + name;
            IList<UsuariosRH> usuariosRHsList = new List<UsuariosRH>();
            var ResponseOK = App.HttpTools.HttpGetList<UsuariosRH>(webApiUrl, ref usuariosRHsList, "No se encontró el usuario");
            if (ResponseOK == HttpStatusCode.OK)
            {
                return usuariosRHsList.ToList();
            }
            else
            {
                return null;
            }
        }
        public List<UsuariosRH> GetAllUsuarios()
        {
            string webApiUrl = WebApiMethods.GetAllUsuarios;
            IList<UsuariosRH> usuariosRHsList = new List<UsuariosRH>();
            var ResponseOK = App.HttpTools.HttpGetList<UsuariosRH>(webApiUrl, ref usuariosRHsList, "No se encontró el usuario");
            if (ResponseOK == HttpStatusCode.OK)
            {
                return usuariosRHsList.ToList();
            }
            else
            {
                return null;
            }
        }

        public async Task<UsuariosRH> SaveUser(UsuariosRH usuario)
        {
            string webApiUrl = WebApiMethods.SaveUser;
            UsuariosRH usuariosResult = await App.HttpTools.HttpPostObjectWithResponseDataAsync<UsuariosRH, UsuariosRH>(webApiUrl, usuario, "Hubo un error en el guardado del usuario").ConfigureAwait(false);
            return usuariosResult;
        }

    }
}
