using checkpoint.Users.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace checkpoint.Users.Services
{
    public interface IUsersServices
    {
        List<UsuariosRH> SearchUsersByName(string name);
        List<UsuariosRH> GetAllUsuarios();
        Task<UsuariosRH> SaveUser(UsuariosRH usuario);
    }
}
