using Pos.Business.Model;
using System.Collections.Generic;

namespace Pos.BAL.Interface.Domain
{
    public interface IUnidadSatRepository : IGenericRepository<UnidadSat>
    {
        IEnumerable<object> GetAllUnidadesSat();
        IEnumerable<object> GetUnidadesSatByid(int unitSatId);
    }
}
