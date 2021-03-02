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
        public async Task<Ventas> AddVenta(Ventas venta)
        {
            string webApiUrl = WebApiMethods.AddVenta;
            Ventas ventaResult = await App.HttpTools.HttpPostObjectWithResponseDataAsync<Ventas, Ventas>(webApiUrl, venta, "Hubo un error en el guardado de la venta").ConfigureAwait(false);
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
    }
}

