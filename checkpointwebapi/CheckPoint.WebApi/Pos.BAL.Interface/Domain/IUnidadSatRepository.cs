using Pos.Business.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.BAL.Interface.Domain
{
    public interface IUnidadSatRepository : IGenericRepository<UnidadSat>
    {
        IEnumerable<object> GetAllUnidadesSat();
        IEnumerable<object> GetUnidadesSatByid(int unitSatId);
    }
}
