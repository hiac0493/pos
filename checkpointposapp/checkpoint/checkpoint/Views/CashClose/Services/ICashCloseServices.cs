using checkpoint.Views.CashClose.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace checkpoint.Views.CashClose.Services
{
    public interface ICashCloseServices
    {
        Task<Cortes> SaveCashClose(Cortes corte);
        int GetLastFolio();
        Cortes GetCurrentCashClose(int idUsuario);
        double GetTotalSale(int idUsuario, long folioInicio, long? folioFin);
        List<CortePagos> GetAllPagosCorte(long folioInicio, long? folioFinal, int IdUsuario);
        List<VentaImpuestos> GetAllTaxes(long folioInicio, long? folioFinal, int IdUsuario);
        double GetTotalTaxes(long folioInicio, long? folioFinal, int IdUsuario);
        List<TasaImpuesto> GetTotalWithTaxes(long folioInicio, long? folioFinal, int IdUsuario);
        List<TasaImpuesto> GetReturnsWithTaxes(long folioInicio, long? folioFinal, int IdUsuario);
        double GetTotalReturns(long folioInicio, long? folioFinal, int IdUsuario);
        double CalcIvaTasa(int IdUsuario, long folioInicio, long? folioFinal);
        double CalcIvaReturn(int IdUsuario, long folioInicio, long? folioFinal);
    }
}
