using Pos.Business.Model;
using System.Collections.Generic;

namespace Pos.BAL.Interface.Domain
{
    public interface ICatalogoSatRepository : IGenericRepository<CatalogoSat>
    {
        IEnumerable<object> GetAllCatalogoSat();
    }
}
