using checkpoint.Sales.Models;
using checkpoint.Sales.Services;
using checkpoint.Views.Sales.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace checkpoint.Sales.Presenters
{
    public class SalesPresenter
    {
        private readonly ISalesServices _salesServices;

        public SalesPresenter(ISalesServices salesServices)
        {
            _salesServices = salesServices;
        }

        public ProductSale GetProductByPLU(string PLU)
        {
            return _salesServices.GetProductByPLU(PLU);
        }

        public Task<SaleResult> AddVenta(Ventas venta)
        {
            return _salesServices.AddVenta(venta);
        }

        public List<TipoPago> GetAllTipoPagos()
        {
            return _salesServices.GetAllTipoPago();
        }

        public Ventas CancelaVenta(CancelacionDto cancelacion)
        {
            return _salesServices.CancelaVenta(cancelacion);
        }
        public double GetTotalEfectivo(int idUsuario)
        {
            return _salesServices.GetTotalEfectivo(idUsuario);
        }
    }
}
