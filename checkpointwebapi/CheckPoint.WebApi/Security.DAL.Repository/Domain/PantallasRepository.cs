using Security.BAL.Interface.Domain;
using Security.Business.Model;
using Security.EntityFramework.Edbm;
using System.Collections.Generic;
using System.Linq;

namespace Security.DAL.Repository.Domain
{
    public class PantallasRepository : GenericRepository<Pantallas>, IPantallasRepository
    {
        public PantallasRepository(SecurityDbContext context) : base(context)
        {
        }

        public SecurityDbContext dbContext
        {
            get { return _context as SecurityDbContext; }
        }

        public IEnumerable<Pantallas> GetAllPrincipalPantallasByUser(int userId)
        {
            List<int> screensUser = dbContext.PantallasUsuario.Where(x => x.idUsuario.Equals(userId)).Select(x => x.idPantalla).ToList();
            return dbContext.Pantallas.Where(x => screensUser.Contains(x.idPantalla) && !x.SubPantalla);
        }

        public IEnumerable<Pantallas> GetPantallasByUserId(int userId)
        {
            List<int> screensUser = dbContext.PantallasUsuario.Where(x => x.idUsuario.Equals(userId)).Select(x => x.idPantalla).ToList();
            return dbContext.Pantallas.Where(x => screensUser.Contains(x.idPantalla));
        }
    }
}
