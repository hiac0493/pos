using Pos.Business.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.BAL.Interface.Domain
{
    public interface IOrdenesRepository : IGenericRepository<Ordenes>
    {
        IEnumerable<object> GetAllOrdersWithSupplier();
        IEnumerable<object> GetOrdersByEstatusWithSupplier(char estatusSearch);
    }
}
