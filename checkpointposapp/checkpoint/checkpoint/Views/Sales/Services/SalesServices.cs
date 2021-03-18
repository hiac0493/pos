using checkpoint.Resources;
using checkpoint.Sales.Models;
using checkpoint.Views.Sales.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace checkpoint.Sales.Services
{
    public class SalesServices : ISalesServices
    {
        public async Task<SaleResult> AddVenta(Ventas venta)
        {
            string webApiUrl = WebApiMethods.AddVenta;
            SaleResult ventaResult = await App.HttpTools.HttpPostObjectWithResponseDataAsync<Ventas, SaleResult>(webApiUrl, venta, "Hubo un error en el guardado de la venta").ConfigureAwait(false);
            return ventaResult;
        }

        public ProductSale GetProductByPLU(string PLU)
        {
            string webApiUrl = WebApiMethods.GetProductByPLU + PLU;
            ProductSale product = new ProductSale();
            var responseOK = App.HttpTools.HttpGetSingle<ProductSale>(webApiUrl, ref product, "No se encontró el producto");

            if (responseOK == HttpStatusCode.OK)
            {
                return product;
            }
            else
            {
                return null;
            }
        }

        public List<TipoPago> GetAllTipoPago()
        {
            string webApiUrl = WebApiMethods.GetAllTipoPago;
            IList<TipoPago> tiposPago = new List<TipoPago>();
            var responseOk = App.HttpTools.HttpGetList<TipoPago>(webApiUrl, ref tiposPago, "Hubo un error en la lectura de los tipos de pago");

            if (responseOk == HttpStatusCode.OK)
            {
                return tiposPago.ToList();
            }
            else
            {
                return null;
            }
        }

        public Ventas CancelaVenta(CancelacionDto cancelacion)
        {
            string webApiUrl = WebApiMethods.CancelaVenta;
            Ventas venta = App.HttpTools.HttpPostObjectWithResponseDataAsync<CancelacionDto, Ventas>(webApiUrl, cancelacion, $"Error en la cancelación de la venta {cancelacion.folioVenta}").Result;
            return venta;
        }

        public double GetTotalEfectivo(int idUsuario)
        {
            string webApiUrl = WebApiMethods.GetTotalEfectivo + idUsuario;
            double sale = 0;
            var response = App.HttpTools.HttpGetSingle(webApiUrl, ref sale, "Hubo un error al obtener el folio");
            if (response == HttpStatusCode.OK)
                return sale;
            else
                return 0;
        }
    }
}

