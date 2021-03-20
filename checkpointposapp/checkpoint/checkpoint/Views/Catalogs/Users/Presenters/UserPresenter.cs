using checkpoint.Users.Models;
using checkpoint.Users.Services;
using Pos.Business.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace checkpoint.Users.Presenters
{
    public class UserPresenter
    {
        private readonly IUsersServices _usersServices;

        public UserPresenter(IUsersServices usersServices)
        {
            _usersServices = usersServices;
        }

        public List<UsuariosRH> SearchUsersByName(string name)
        {
            return _usersServices.SearchUsersByName(name);
        }        
        
        public List<UsuariosRH> GetAllUsuarios()
        {
            return _usersServices.GetAllUsuarios();
        }
        public List<TipoUsuario> GetAllUserTypes()
        {
            return _usersServices.GetAllUserTypes();
        }

        public Task<UsuariosRH> SaveUser(UsuariosRH usuario)
        {
            return _usersServices.SaveUser(usuario);
        }
    }
}
