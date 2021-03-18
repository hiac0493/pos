using Pos.Business.Model;
using System.Collections.Generic;

namespace Pos.BAL.Interface.Domain
{
    public interface IUsuariosRepository : IGenericRepository<Usuarios>
    {
        Usuarios GetUsuarioByUserName(string userName);
        IEnumerable<object> GetUsuarioByName(string name);
        bool GetUsuarioAdmin(string user, string pass);
        IEnumerable<Usuarios> GetAllUsuariosWithPermissions();
    }
}
