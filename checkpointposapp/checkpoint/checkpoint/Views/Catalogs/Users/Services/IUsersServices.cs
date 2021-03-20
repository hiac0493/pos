using checkpoint.Users.Models;
using Pos.Business.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace checkpoint.Users.Services
{
    public interface IUsersServices
    {
        List<UsuariosRH> SearchUsersByName(string name);
        List<UsuariosRH> GetAllUsuarios();
        List<TipoUsuario> GetAllUserTypes();
        Task<UsuariosRH> SaveUser(UsuariosRH usuario);
    }
}
