using checkpoint.Sales.Models;
using checkpoint.Views.Sales.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace checkpoint.Sales.Services
{
    public interface ISalesServices
    {
        ProductSale GetProductByPLU(string PLU);
        Task<SaleResult> AddVenta(Ventas venta);
        List<TipoPago> GetAllTipoPago();
        Ventas CancelaVenta(CancelacionDto cancelacion);
        public double GetTotalEfectivo(int idUsuario);
    }
}
