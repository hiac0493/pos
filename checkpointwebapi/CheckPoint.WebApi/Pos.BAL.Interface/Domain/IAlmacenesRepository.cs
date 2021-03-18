using Pos.Business.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.BAL.Interface.Domain
{
    public interface IAlmacenesRepository : IGenericRepository<Almacenes>
    {
        IEnumerable<object> GetProductosByAlmacen(int idAlmacen);
    }
}
