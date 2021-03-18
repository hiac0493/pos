using checkpoint.CancelSales.Models;
using checkpoint.Resources;
using checkpoint.Views.CancelSales.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace checkpoint.Views.CancelSales.Services
{
    public class CancelSaleServices : ICancelSaleServices
    {
        public Ventas CancelaVenta(CancelacionDto cancelacion)
        {
            string webApiUrl = WebApiMethods.CancelaVenta;
            Ventas venta = App.HttpTools.HttpPostObjectWithResponseDataAsync<CancelacionDto, Ventas>(webApiUrl, cancelacion, $"Error en la cancelación de la venta {cancelacion.folioVenta}").Result;
            return venta;
        }

        public Ventas GetVentaByFolio(long folioVenta)
        {
            string webApiUrl = WebApiMethods.GetVentaByFolioVenta + folioVenta;
            Ventas ventaResult = new Ventas();
            var result = App.HttpTools.HttpGetSingle<Ventas>(webApiUrl, ref ventaResult, "Error al leer la venta");
            return ventaResult;
        }
    }
}
