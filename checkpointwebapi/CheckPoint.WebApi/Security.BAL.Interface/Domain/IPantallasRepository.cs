using Security.Business.Model;
using System.Collections.Generic;

namespace Security.BAL.Interface.Domain
{
    public interface IPantallasRepository : IGenericRepository<Pantallas>
    {
        IEnumerable<Pantallas> GetPantallasByUserId(int userId);
        IEnumerable<Pantallas> GetAllPrincipalPantallasByUser(int userId);
    }
}
