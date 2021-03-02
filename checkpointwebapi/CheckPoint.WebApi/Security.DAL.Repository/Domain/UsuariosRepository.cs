using Security.BAL.Interface.Domain;
using Security.Business.Model;
using Security.EntityFramework.Edbm;
using System.Linq;

namespace Security.DAL.Repository.Domain
{
    public class UsuariosRepository : GenericRepository<Usuarios>, IUsuariosRepository
    {
        public UsuariosRepository(SecurityDbContext context) : base(context)
        {
        }
        public Usuarios GetUsuarioByUserName(string userName)
        {
            return dbContext.Usuarios.Where(x => x.NombreUsuario.Equals(userName)).SingleOrDefault();
        }

        public SecurityDbContext dbContext
        {
            get { return _context as SecurityDbContext; }
        }
    }
}
