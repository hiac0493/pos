using checkpoint.CancelSales.Models;
using checkpoint.Views.CancelSales.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace checkpoint.Views.CancelSales.Services
{
    public interface ICancelSaleServices
    {
        Ventas CancelaVenta(CancelacionDto cancelacion);
        Ventas GetVentaByFolio(long folioVenta);
    }
}
