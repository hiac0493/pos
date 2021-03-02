using Pos.Business.Model;
using System.Collections.Generic;

namespace Pos.BAL.Interface.Domain
{
    public interface IVentasRepository : IGenericRepository<Ventas>
    {
        Ventas GetVentaByFolio(long folioVenta);
        double GetTotalSale(int idUsuario, long folioInicio, long folioFin);
        long GetLastFolio();
        IEnumerable<object> GetAllPagosCorte(long folioInicio, long folioFinal, int IdUsuario);
        object GetTotalWithTaxes(long folioInicio, long folioFinal, int IdUsuario);
        object GetReturnsWithTaxes(long folioInicio, long folioFinal, int IdUsuario);
        double CalcIvaTasa(int idUsuario, long folioInicio, long folioFin);
        double CalcIvaReturn(int idUsuario, long folioInicio, long folioFin);
        double GetTotalReturn(int idUsuario, long folioInicio, long folioFin);
        

    }
}
