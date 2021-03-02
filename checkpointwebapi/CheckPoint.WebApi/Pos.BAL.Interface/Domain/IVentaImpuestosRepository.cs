using Pos.Business.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pos.BAL.Interface.Domain
{
    public interface IVentaImpuestosRepository : IGenericRepository<VentaImpuestos>
    {
        IEnumerable<object> GetAllTaxes(long folioInicio, long folioFinal, int IdUsuario);
        double GetTotalTaxes(long folioInicio, long folioFinal, int IdUsuario);

    }
}
