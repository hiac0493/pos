using Pos.Business.Model;
using System.Collections.Generic;

namespace Pos.BAL.Interface.Domain
{
    public interface IDepartamentosRepository : IGenericRepository<Departamentos>
    {
        IEnumerable<object> getDepartmenByName(string name);
    }
}
