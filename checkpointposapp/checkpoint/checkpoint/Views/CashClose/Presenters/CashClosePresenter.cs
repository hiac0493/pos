using checkpoint.Views.CashClose.Models;
using checkpoint.Views.CashClose.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace checkpoint.Views.CashClose.Presenters
{
    class CashClosePresenter
    {
        private readonly ICashCloseServices _cashCloseServices;
        public CashClosePresenter(ICashCloseServices cashCloseServices)
        {
            _cashCloseServices = cashCloseServices;
        }

        public Task<Cortes> SaveCashClose(Cortes corte)
        {
            return _cashCloseServices.SaveCashClose(corte);
        }

        public int GetLastFolio()
        {
            return _cashCloseServices.GetLastFolio();
        }

        public Cortes GetCurrentCashClose(int idUsuario)
        {
            return _cashCloseServices.GetCurrentCashClose(idUsuario);
        }

        public double GetTotalSale(int idUsuario, long folioInicio, long folioFin)
        {
            return _cashCloseServices.GetTotalSale(idUsuario, folioInicio, folioFin);
        }

        public IEnumerable<CortePagos> GetAllPagosCorte(long folioInicio, long folioFinal, int IdUsuario)
        {
            return _cashCloseServices.GetAllPagosCorte(folioInicio, folioFinal, IdUsuario);
        }

        public IEnumerable<VentaImpuestos> GetAllTaxes(long folioInicio, long folioFinal, int IdUsuario)
        {
            return _cashCloseServices.GetAllTaxes(folioInicio, folioFinal, IdUsuario);
        }

        public double GetTotalTaxes(long folioInicio, long folioFinal, int IdUsuario)
        {
            return _cashCloseServices.GetTotalTaxes(folioInicio, folioFinal, IdUsuario);
        }
        public IEnumerable<TasaImpuesto> GetTotalWithTaxes(long folioInicio, long folioFinal, int IdUsuario)
        {
            return _cashCloseServices.GetTotalWithTaxes(folioInicio, folioFinal, IdUsuario);
        }
        public IEnumerable<TasaImpuesto> GetReturnsWithTaxes(long folioInicio, long folioFinal, int IdUsuario)
        {
            return _cashCloseServices.GetReturnsWithTaxes(folioInicio, folioFinal, IdUsuario);
        }
        public double GetTotalReturns(long folioInicio, long folioFinal, int IdUsuario)
        {
            return _cashCloseServices.GetTotalReturns(folioInicio, folioFinal, IdUsuario);
        }

        public double CalcIvaTasa(int IdUsuario, long folioInicio, long folioFinal)
        {
            return _cashCloseServices.CalcIvaTasa(IdUsuario, folioInicio, folioFinal);
        }

        public double CalcIvaReturn(int IdUsuario, long folioInicio, long folioFinal)
        {
            return _cashCloseServices.CalcIvaReturn(IdUsuario, folioInicio, folioFinal);
        }
    }
}
