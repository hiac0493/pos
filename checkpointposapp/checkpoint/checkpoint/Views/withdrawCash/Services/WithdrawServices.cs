using checkpoint.Resources;
using checkpoint.Views.CashClose.Models;
using checkpoint.Views.withdrawCash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace checkpoint.Views.withdrawCash.Services
{
    public class WithdrawServices : IWithdrawServices
    {
        public async Task<Retiros> SaveRetiros(Retiros retiro)
        {
            string webApiUrl = WebApiMethods.SaveRetiro;
            Retiros response = await App.HttpTools.HttpPostObjectWithResponseDataAsync<Retiros, Retiros>(webApiUrl, retiro, "Hubo un error al guardar el corte").ConfigureAwait(false);
            return response;
        }

        public Cortes GetCurrentCashClose(int idUsuario)
        {
            string webApiUrl = WebApiMethods.GetCurrentCashClose + idUsuario;
            Cortes model = new Cortes();
            HttpStatusCode ResponseOk = App.HttpTools.HttpGetSingle(webApiUrl, ref model, "Hubo un error al obtener el corte");
            if (ResponseOk == HttpStatusCode.OK)
            {
                return model ?? null;
            }
            else
            {
                return null;
            }
        }

        public List<Retiros> GetAllRetiros(long idcorte)
        {
            string webApiUrl = WebApiMethods.GetAllRetiros + idcorte;
            IList<Retiros> retiros = new List<Retiros>();
            var ResponseOk = App.HttpTools.HttpGetList<Retiros>(webApiUrl, ref retiros, "Hubo un error en la lectura de las unidades");
            if (ResponseOk == HttpStatusCode.OK)
            {
                return retiros.ToList();
            }
            else
            {
                return null;
            }
        }
        public List<Retiros> GetAllCashIncomes(long idcorte)
        {
            string webApiUrl = WebApiMethods.GetAllCashIncomes + idcorte;
            IList<Retiros> retiros = new List<Retiros>();
            var ResponseOk = App.HttpTools.HttpGetList<Retiros>(webApiUrl, ref retiros, "Hubo un error en la lectura de las unidades");
            if (ResponseOk == HttpStatusCode.OK)
            {
                return retiros.ToList();
            }
            else
            {
                return null;
            }
        }
    }
}
