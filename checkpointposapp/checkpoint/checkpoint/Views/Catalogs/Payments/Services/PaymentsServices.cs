using checkpoint.Resources;
using checkpoint.Views.Catalogs.Payments.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace checkpoint.Views.Catalogs.Payments.Services
{
    public class PaymentsServices : IPaymentsServices
    {
        public IEnumerable<TipoPago> getAllPayments()
        {
            string webApiUrl = WebApiMethods.GetAllTipoPago;
            IList<TipoPago> tipoPagoList = new List<TipoPago>();
            var ResponseOK = App.HttpTools.HttpGetList<TipoPago>(webApiUrl, ref tipoPagoList, "No se encontró el tipo de pago");
            if (ResponseOK == HttpStatusCode.OK)
            {
                return tipoPagoList.ToList();
            }
            else
            {
                return null;
            }
        }

        public List<TipoPago> getPaymentByName(string name)
        {
            string webApiUrl = WebApiMethods.GetPaymentByName + name;
            IList<TipoPago> tipoPagoList = new List<TipoPago>();
            var ResponseOk = App.HttpTools.HttpGetList<TipoPago>(webApiUrl, ref tipoPagoList, "No se encontró el tipo de pago");
            if (ResponseOk == HttpStatusCode.OK)
            {
                return tipoPagoList.ToList();
            }
            else
            {
                return null;
            }
        }

        public async Task<TipoPago> savePayment(TipoPago tipoPago)
        {
            string webApiUrl = WebApiMethods.SavePayment;
            TipoPago tipoPagoResult = await App.
                HttpTools.HttpPostObjectWithResponseDataAsync<TipoPago, TipoPago>(webApiUrl, tipoPago, "Hubo un error en el guardado del metodo de pago").ConfigureAwait(false);
            return tipoPagoResult;
        }
    }
}
