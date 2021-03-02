using Security.Business.Model;

namespace Security.BAL.Interface.Domain
{
    public interface IUsuariosRepository : IGenericRepository<Usuarios>
    {
        Usuarios GetUsuarioByUserName(string userName);
    }
}
