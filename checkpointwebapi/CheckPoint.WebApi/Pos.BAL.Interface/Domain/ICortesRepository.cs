using Pos.Business.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.BAL.Interface.Domain
{
    public interface ICortesRepository : IGenericRepository<Cortes>
    {
        Cortes GetCurrentCashClose(int usuario);
        object GetCorteInfo(Cortes corte);
    }
}
