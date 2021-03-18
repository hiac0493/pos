using checkpoint.CancelSales.Models;
using checkpoint.Views.CancelSales.Models;
using checkpoint.Views.CancelSales.Services;

namespace checkpoint.Views.CancelSales.Presenters
{
    public class CancelSalePresenter
    {
        private readonly ICancelSaleServices _cancelSaleServices;

        public CancelSalePresenter(ICancelSaleServices cancelSaleServices)
        {
            _cancelSaleServices = cancelSaleServices;
        }

        public Ventas GetVentaByFolioVenta(long folioVenta)
        {
            return _cancelSaleServices.GetVentaByFolio(folioVenta);
        }

        public Ventas CancelaVenta(long folioVenta)
        {
            return _cancelSaleServices.CancelaVenta(new CancelacionDto
            {
                idUsuario = App._userApplication.idUsuario,
                folioVenta = folioVenta
            });                 
        }
    }
}
