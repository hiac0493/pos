using Security.BAL.Interface.Domain;
using Security.Business.Model;
using Security.EntityFramework.Edbm;

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
    }
}
