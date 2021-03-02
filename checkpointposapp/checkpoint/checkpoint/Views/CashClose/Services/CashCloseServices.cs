using checkpoint.Resources;
using checkpoint.Views.CashClose.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace checkpoint.Views.CashClose.Services
{
    public class CashCloseServices : ICashCloseServices
    {

        public async Task<Cortes> SaveCashClose(Cortes corte)
        {
            string webApiUrl = WebApiMethods.SaveCashClose;
            Cortes response = await App.HttpTools.HttpPostObjectWithResponseDataAsync<Cortes, Cortes>(webApiUrl, corte, "Hubo un error al guardar el corte").ConfigureAwait(false);
            return response;
        }

        public int GetLastFolio()
        {
            string webApiUrl = WebApiMethods.GetLastFolio;
            int folio = 0;
            var response = App.HttpTools.HttpGetSingle(webApiUrl, ref folio, "Hubo un error al obtener el folio");
            if (response == HttpStatusCode.OK)
                return folio;
            else
                return 0;
        }



        public Cortes GetCurrentCashClose(int idUsuario)
        {
            string webApiUrl = WebApiMethods.GetCurrentCashClose + idUsuario;
             Cortes model = new Cortes();
            HttpStatusCode ResponseOk = App.HttpTools.HttpGetSingle(webApiUrl, ref model, "Hubo un error en la lectura de las unidades");
            if (ResponseOk == HttpStatusCode.OK)
            {
                return model != null ? model : null;
            }
            else
            {
                return null;
            }
        }

        public double GetTotalSale(int idUsuario, long folioInicio, long folioFin)
        {
            string webApiUrl = WebApiMethods.GetTotalSale+ $"?idUsuario={idUsuario}&folioInicio={folioInicio}&folioFin={folioFin}";
            double sale = 0;
            var response = App.HttpTools.HttpGetSingle(webApiUrl, ref sale, "Hubo un error al obtener el folio");
            if (response == HttpStatusCode.OK)
                return sale;
            else
                return 0;
        }

        public List<CortePagos> GetAllPagosCorte(long folioInicio, long folioFinal, int IdUsuario)
        {
            string webApiUrl = WebApiMethods.GetAllPagosCorte + $"?folioInicio={folioInicio}&folioFinal={folioFinal}&IdUsuario={IdUsuario}";
            IList<CortePagos> infolist = new List<CortePagos>();
            var ResponseOK = App.HttpTools.HttpGetList<CortePagos>(webApiUrl, ref infolist, "No se encontraron los pagos");
            if (ResponseOK == HttpStatusCode.OK)
            {
                return infolist.ToList();
            }
            else
            {
                return null;
            }

        }

        public List<VentaImpuestos> GetAllTaxes(long folioInicio, long folioFinal, int IdUsuario)
        {
            string webApiUrl = WebApiMethods.GetAllTaxes + $"?folioInicio={folioInicio}&folioFinal={folioFinal}&IdUsuario={IdUsuario}";
            IList<VentaImpuestos> infolist = new List<VentaImpuestos>();
            var ResponseOK = App.HttpTools.HttpGetList<VentaImpuestos>(webApiUrl, ref infolist, "No se encontraron los impuestos");
            if (ResponseOK == HttpStatusCode.OK)
            {
                return infolist.ToList();
            }
            else
            {
                return null;
            }
        }

        public double GetTotalTaxes(long folioInicio, long folioFinal, int IdUsuario)
        {
            string webApiUrl = WebApiMethods.GetTotalTaxes + $"?folioInicio={folioInicio}&folioFinal={folioFinal}&IdUsuario={IdUsuario}";
            double sale = 0;
            var response = App.HttpTools.HttpGetSingle(webApiUrl, ref sale, "Hubo un error al obtener los impuestos");
            if (response == HttpStatusCode.OK)
                return sale;
            else
                return 0;
        }
        public List<TasaImpuesto> GetTotalWithTaxes(long folioInicio, long folioFinal, int IdUsuario)
        {
            string webApiUrl = WebApiMethods.GetTotalWithTaxes + $"?folioInicio={folioInicio}&folioFinal={folioFinal}&IdUsuario={IdUsuario}";
            IList<TasaImpuesto> infolist = new List<TasaImpuesto>();
            var ResponseOK = App.HttpTools.HttpGetList<TasaImpuesto>(webApiUrl, ref infolist, "No se encontraron los impuestos");
            if (ResponseOK == HttpStatusCode.OK)
            {
                return infolist.ToList();
            }
            else
            {
                return null;
            }
        }
        public List<TasaImpuesto> GetReturnsWithTaxes(long folioInicio, long folioFinal, int IdUsuario)
        {
            string webApiUrl = WebApiMethods.GetReturnsWithTaxes + $"?folioInicio={folioInicio}&folioFinal={folioFinal}&IdUsuario={IdUsuario}";
            IList<TasaImpuesto> infolist = new List<TasaImpuesto>();
            var ResponseOK = App.HttpTools.HttpGetList<TasaImpuesto>(webApiUrl, ref infolist, "No se encontraron los impuestos");
            if (ResponseOK == HttpStatusCode.OK)
            {
                return infolist.ToList();
            }
            else
            {
                return null;
            }
        }
        public double GetTotalReturns(long folioInicio, long folioFinal, int IdUsuario)
        {
            string webApiUrl = WebApiMethods.GetTotalReturns + $"?folioInicio={folioInicio}&folioFinal={folioFinal}&IdUsuario={IdUsuario}";
            double sale = 0;
            var response = App.HttpTools.HttpGetSingle(webApiUrl, ref sale, "Hubo un error al obtener los impuestos");
            if (response == HttpStatusCode.OK)
                return sale;
            else
                return 0;
        }
        public double CalcIvaTasa(int IdUsuario, long folioInicio, long folioFinal)
        {
            string webApiUrl = WebApiMethods.CalcIvaTasa + $"?IdUsuario={IdUsuario}&folioInicio={folioInicio}&folioFinal={folioFinal}";
            double sale = 0;
            var response = App.HttpTools.HttpGetSingle(webApiUrl, ref sale, "Hubo un error al obtener los impuestos");
            if (response == HttpStatusCode.OK)
                return sale;
            else
                return 0;
        }
        public double CalcIvaReturn(int IdUsuario, long folioInicio, long folioFinal)
        {
            string webApiUrl = WebApiMethods.CalcIvaReturn + $"?IdUsuario={IdUsuario}&folioInicio={folioInicio}&folioFinal={folioFinal}";
            double sale = 0;
            var response = App.HttpTools.HttpGetSingle(webApiUrl, ref sale, "Hubo un error al obtener los impuestos");
            if (response == HttpStatusCode.OK)
                return sale;
            else
                return 0;
        }




    }
}
